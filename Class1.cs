using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using OfficeOpenXml;
using System.Diagnostics;
using OfficeOpenXml.Style;
using Cryptage;

namespace Cryptage
{
    public class Cryptage_Application : IExternalApplication
    {
        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            string myRibbon = "Cryptage";
            try
            {
                application.CreateRibbonTab(myRibbon);
            }
            catch 
            {
            }

            RibbonPanel panel1 = application.CreateRibbonPanel(myRibbon, "Action sur le modèle");            
            PushButton cryptage = (PushButton)panel1.AddItem(new PushButtonData("Cryptage", "Cryptage du modèle", path, "Cryptage.Cryptage_Class"));
            cryptage.LargeImage = PngImageSource("Cryptage.Resources.Cryptage32.png");
            //cryptage.Image = PngImageSource("Space_Manager.Resources.Space_Creation_16.png");
            //space_creation.ToolTipImage = PngImageSource("Space_Manager.Resources.Space_Creation_16.png");
            cryptage.ToolTip = "Crypter la maquette.";
            cryptage.LongDescription = "Choisir les options de cryptage de la maquette.";

            PushButton décryptage = (PushButton)panel1.AddItem(new PushButtonData("Décryptage", "Décryptage du modèle", path, "Cryptage.Décryptage_Class"));
            décryptage.LargeImage = PngImageSource("Cryptage.Resources.Décryptage32.png");
            //décryptage.Image = PngImageSource("Space_Manager.Resources.Space_Creation_16.png");
            //space_creation.ToolTipImage = PngImageSource("Space_Manager.Resources.Space_Creation_16.png");
            décryptage.ToolTip = "Décrypter la maquette.";
            décryptage.LongDescription = "Revenir à l'état initial du modèle à l'aide d'une clé de déchiffrement.";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }

        private System.Windows.Media.ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }
    }
  
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class Cryptage_Class : IExternalCommand
    {
        static AddInId appId = new AddInId(new Guid("4a143591-b59d-4bb2-b0c8-0367c92e351a"));
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
           
            List<string> liste = new List<string>();           
           
            Form1 form = new Form1(doc);
            bool RoomName = false;
            bool RoomWalls = false;
            bool RoomFurniture = false;
            bool RoomDoors = false;
            bool RoomAddFurniture = false;
            bool RoomFinishes = false;

            if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                return Result.Cancelled;
            }

            if (DialogResult.OK == form.ShowDialog())
            {
                //Nom du paramètre de type "oui/non" permettant de détecter les locaux confidentiels
                string param = "G6_Confidentiel";
                //Nom des éléments pour le remplacement des familles
                string wall_type = "XXXX";
                string door_type = "XXXX_P:XXXX";
                string furniture_type = "XXXX_M:XXXX";
                string plumbing_type = "XXXX_S:XXXX";
                string furniture_param = "Dimension";
                string plumbing_param = "Dimension";
                //Valeur attribuée aux paramètres cryptés
                string change = "XXXX";
                //Clé de décryptage
                List<string> key = new List<string>();
                string phase = "Nouvelle construction";
                string phase2 = "New Construction";

                ElementId levelID = null;
                FamilySymbol door_fs = null;
                FamilySymbol furniture_fs = null;
                FamilySymbol plumbing_fs = null;
                ElementId eid = null;
                Line newWallLine_2 = null;
                List<Curve> curves = new List<Curve>();
                int count = 0;
                Random rnd = new Random();
                int Offset = rnd.Next(4, 6);
                double random = rnd.NextDouble() * Offset;
                //double random = Offset;

                using (Transaction t = new Transaction(doc, "Crypter la maquette"))
                {
                    t.Start();
                    RoomName = form.RoomName();
                    RoomWalls = form.RoomWalls();
                    RoomFurniture = form.RoomFurniture();
                    RoomDoors = form.RoomDoors();
                    RoomAddFurniture = form.RoomAddFurnitures();
                    RoomFinishes = form.RoomFinishes();
                    //Sélection du type de porte de remplacement
                    FamilySymbol fs = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_Doors).Cast<FamilySymbol>()
                                       where element.Family.Name + ":" + element.Name == door_type
                                       select element).ToList<FamilySymbol>().First();
                    door_fs = fs;
                    //Sélection du type de mobilier de remplacement
                    FamilySymbol fs_furniture = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_Furniture).Cast<FamilySymbol>()
                                                 where element.Family.Name + ":" + element.Name == furniture_type
                                                 select element).ToList<FamilySymbol>().First();
                    furniture_fs = fs_furniture;
                    //Sélection du type de sanitaire de remplacement
                    FamilySymbol fs_plumbing = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_PlumbingFixtures).Cast<FamilySymbol>()
                                                 where element.Family.Name + ":" + element.Name == plumbing_type
                                                 select element).ToList<FamilySymbol>().First();
                    plumbing_fs = fs_plumbing;
                    //Sélection du type de mur de remplacement
                    WallType walltype = (from element in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType()
                                         where element.Name == wall_type
                                         select element).Cast<WallType>().ToList<WallType>().First();
                    eid = walltype.Id;
                    //Remplacement du mobilier
                    if (RoomFurniture==true)
                    {
                    foreach (FamilyInstance furniture_instance in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Furniture).Cast<FamilyInstance>())
                    {
                        if (furniture_instance.Room != null && furniture_instance.Room.LookupParameter(param).AsInteger() == 1)
                        {
                            try
                                {
                            string original = furniture_instance.Name;
                            furniture_instance.Symbol = furniture_fs;
                            Random rd = new Random();
                            double random_dimension = 0.5 + rd.NextDouble();
                            furniture_instance.LookupParameter(furniture_param).Set(UnitUtils.ConvertToInternalUnits(random_dimension, DisplayUnitType.DUT_METERS));
                            key.Add("F;" + furniture_instance.Id.ToString() + ";" + original + ";" + furniture_instance.Room.Id.ToString() + ";" + furniture_instance.Room.Name);
                                }
                                catch
                                {
                                }
                            }
                    }
                        foreach (FamilyInstance plumbing_instance in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_PlumbingFixtures).Cast<FamilyInstance>())
                        {
                            if (plumbing_instance.Room != null && plumbing_instance.Room.LookupParameter(param).AsInteger() == 1)
                            {                             
                                    string original = plumbing_instance.Name;
                                    plumbing_instance.Symbol = plumbing_fs;
                                    Random rd = new Random();
                                    double random_dimension = 0.5 + rd.NextDouble();
                                    plumbing_instance.LookupParameter(plumbing_param).Set(UnitUtils.ConvertToInternalUnits(random_dimension, DisplayUnitType.DUT_METERS));
                                    key.Add("P;" + plumbing_instance.Id.ToString() + ";" + original + ";" + plumbing_instance.Room.Id.ToString() + ";" + plumbing_instance.Room.Name);                             
                            }
                        }
                    }
                    //Lister les pièces confidentielles et appartenant à la phase désignée
                    List<Room> rooms = (from element in new FilteredElementCollector(doc).OfClass(typeof(SpatialElement)).OfCategory(BuiltInCategory.OST_Rooms).Cast<Room>()
                                        where element.LookupParameter(param).AsInteger() == 1
                                        && (element.get_Parameter(BuiltInParameter.ROOM_PHASE).AsValueString() == phase || element.get_Parameter(BuiltInParameter.ROOM_PHASE).AsValueString() == phase2)
                                        select element).ToList<Room>();
                    //Première boucle sur les pièces confidentielles pour renseigner la clé	
                    foreach (Room r in rooms)
                    {
                        Parameter name = r.get_Parameter(BuiltInParameter.ROOM_NAME);
                        Parameter number = r.get_Parameter(BuiltInParameter.ROOM_NUMBER);
                        Parameter floor = r.get_Parameter(BuiltInParameter.ROOM_FINISH_FLOOR);
                        LocationPoint lp = r.Location as LocationPoint;
                        XYZ point = lp.Point;
                        //Ajout des valeurs de paramètres initiaux à la clé de déchiffrement
                        key.Add("R;" + r.Id.ToString() + ";" + name.AsString() + ";" + number.AsString() + ";" + floor.AsString() + ";" + point.X + ";" + point.Y + ";" + point.Z);
                    }

                    //Deuxième boucle sur les pièces confidentielles pour modifier paramètres et géométrie	
                    foreach (Room r in rooms)
                    {
                        Parameter name = r.get_Parameter(BuiltInParameter.ROOM_NAME);
                        Parameter number = r.get_Parameter(BuiltInParameter.ROOM_NUMBER);
                        Parameter floor = r.get_Parameter(BuiltInParameter.ROOM_FINISH_FLOOR);
                        LocationPoint lp = r.Location as LocationPoint;
                        XYZ point = lp.Point;
                        //Remplacement des valeurs de paramètres des pièces par le code
                        if (RoomName == true)
                        {
                        name.Set(change);
                        number.Set(change + count.ToString());
                        }
                        if (RoomFinishes == true)
                        {
                            floor.Set(change);
                        }
                        //Incrémentation du nombre de pièces cryptées
                        count += 1;
                        //Retrouver les limites des pièces
                        IList<IList<Autodesk.Revit.DB.BoundarySegment>> segments = r.GetBoundarySegments(new SpatialElementBoundaryOptions());
                        int segments_number = 0;
                        int boundaries_number = 0;
                        BoundarySegment bd0 = null;
                        BoundarySegment bd1 = null;
                        BoundarySegment bd2 = null;

                        if (null != segments)  //la pièce peut ne pas être fermée
                        {
                            foreach (IList<Autodesk.Revit.DB.BoundarySegment> segmentList in segments)
                            {
                                segments_number += 1;
                                foreach (Autodesk.Revit.DB.BoundarySegment boundarySegment in segmentList)
                                {
                                    boundaries_number += 1;
                                    if (boundaries_number == 1)
                                        bd0 = boundarySegment;
                                    if (boundaries_number == 3)
                                        bd1 = boundarySegment;
                                    if (boundaries_number == 4)
                                        bd2 = boundarySegment;
                                    //Retrouver l'élément qui forme la limite de pièce
                                    Element elt = r.Document.GetElement(boundarySegment.ElementId);
                                    //Déterminer si cet élément est un mur
                                    bool isWall = false;
                                    if (elt as Wall != null)
                                        isWall = true;
                                    //Si c'est un mur, continuer
                                    if (isWall == true)
                                    {
                                        Wall wall = elt as Wall;
                                        Parameter function = wall.WallType.get_Parameter(BuiltInParameter.FUNCTION_PARAM);
                                        //Ne traiter que les murs intérieurs
                                        if (function.AsValueString() == "Interior" || function.AsValueString() == "Intérieur")
                                        {
                                            if (RoomDoors == true)
                                            {
                                                //Changer les types de portes
                                                IList<ElementId> inserts = (wall as HostObject).FindInserts(true, true, true, true);
                                            foreach (ElementId id in inserts)
                                            {
                                                Element e = doc.GetElement(id);
                                                try
                                                {
                                                    FamilyInstance fi = e as FamilyInstance;
                                                    if (fi.get_Parameter(BuiltInParameter.PHASE_CREATED).AsValueString() == phase|| fi.get_Parameter(BuiltInParameter.PHASE_CREATED).AsValueString() == phase2)
                                                    {
                                                        string original = fi.Name;
                                                        fi.Symbol = door_fs;
                                                        LocationPoint doorPoint = fi.Location as LocationPoint;
                                                        XYZ pt = doorPoint.Point;
                                                        key.Add("D;" + fi.Id.ToString() + ";" + original + ";" + pt.X + ";" + pt.Y + ";" + pt.Z);
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            }
                                            //Récupérer les points de départ et d'extrémité des murs
                                            LocationCurve wallLine = wall.Location as LocationCurve;
                                            XYZ endPoint0 = wallLine.Curve.GetEndPoint(0);
                                            XYZ endPoint1 = wallLine.Curve.GetEndPoint(1);
                                            //Ajout des coordonnées initiales des murs à la clé de déchiffrement
                                            key.Add("W;" + wall.Id.ToString() + ";" + wall.WallType.Name + ";" + endPoint0.X.ToString() + ";" + endPoint0.Y.ToString() + ";" + endPoint1.X.ToString() + ";" + endPoint1.Y.ToString());
                                            levelID = wall.LevelId;
                                            //Changer le type des murs
                                            if (RoomWalls == true)
                                               wall.ChangeTypeId(eid);
    
                                        }
                                    }

                                }
                            }

                            if (RoomAddFurniture == true && r.Area > UnitUtils.ConvertToInternalUnits(7, DisplayUnitType.DUT_SQUARE_METERS))
                            {
                            //Créer de nouveaux meubles de façon aléatoire dans les pièces confidentielles
                            Random rd = new Random();
                            int offset = rnd.Next(0, 10);
                            XYZ new_point = new XYZ(point.X + offset * 0.5, point.Y + offset * 0.3, point.Z);
                            if (offset > 1 && offset < 9)
                            {
                                FamilyInstance nf = doc.Create.NewFamilyInstance(new_point, furniture_fs, r.Level, StructuralType.NonStructural);
                                key.Add("NF;" + nf.Id.ToString());
                            }
                            }
                        }

                    }                    

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Title = "Choisir le chemin du fichier de chiffrement";
                    saveFileDialog1.Filter = "Fichier texte (*.txt)|*.txt";
                    saveFileDialog1.RestoreDirectory = true;

                    DialogResult result = saveFileDialog1.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        string txt_filename = saveFileDialog1.FileName;
                        if (File.Exists(txt_filename))
                        {
                            File.Delete(txt_filename);
                        }
                        //Ecrire dans le fichier clé    	
                        using (StreamWriter sw = new StreamWriter(txt_filename))
                        {
                            foreach (string s in key)
                                sw.WriteLine(s);
                        }
                        //Process.Start(txt_filename);
                    }
                    t.Commit();
                    return Result.Succeeded;
                    
                }
                
            }
            return Result.Succeeded;
        }
     }


[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

public class Décryptage_Class : IExternalCommand
{
    static AddInId appId = new AddInId(new Guid("698df198-0be3-4062-9c7d-986b096b5b60"));

    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
    {
        Document doc = commandData.Application.ActiveUIDocument.Document;
        List<string> key = new List<string>();

        using (Transaction t = new Transaction(doc, "Décrypter la maquette"))
        {
            t.Start();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Sélectionner la clé de déchiffrement";
            openFileDialog1.Filter = "Fichier texte (*.txt)|*.txt";
            openFileDialog1.RestoreDirectory = true;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
            string txt_filename = openFileDialog1.FileName;                
            //Lire le fichier Clé.txt 
            int counter = 0;
            string line;
            List<ElementId> to_delete = new List<ElementId>();
            
            System.IO.StreamReader file0 = new System.IO.StreamReader(txt_filename);
            while ((line = file0.ReadLine()) != null)
            {
                key.Add(line);
                string[] list = line.Split(';');
                //Supprimer les murs et mobiliers crées 
                if (line.StartsWith("NW;"))
                {
                    Wall wall = (from element in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType()
                                 where element.Id.ToString() == list[1]
                                 select element).Cast<Wall>().ToList<Wall>().First();
                    to_delete.Add(wall.Id);
                }
                if (line.StartsWith("NF;"))
                {
                    FamilyInstance nf = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Furniture)
                                         where element.Id.ToString() == list[1]
                                         select element).Cast<FamilyInstance>().ToList<FamilyInstance>().First();
                    to_delete.Add(nf.Id);
                }
                counter++;
            }
            file0.Close();

            foreach (ElementId eid in to_delete)
            {
                doc.Delete(eid);
            }

            //Récupérer les valeurs de paramètre initiales pour les murs, à partir du fichier Clé.txt
            System.IO.StreamReader file = new System.IO.StreamReader(txt_filename);
            while ((line = file.ReadLine()) != null)
            {
                key.Add(line);
                string[] list = line.Split(';');
                if (line.StartsWith("W;"))
                {
                    Wall wall = (from element in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType()
                                 where element.Id.ToString() == list[1]
                                 select element).Cast<Wall>().ToList<Wall>().First();
                    WallType walltype = (from element in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType()
                                         where element.Name == list[2]
                                         select element).Cast<WallType>().ToList<WallType>().First();
                    wall.ChangeTypeId(walltype.Id);
                    LocationCurve wallLine = wall.Location as LocationCurve;
                    XYZ endPoint2 = wallLine.Curve.GetEndPoint(0);
                    XYZ endPoint3 = wallLine.Curve.GetEndPoint(1);
                    XYZ endPoint0 = new XYZ(Convert.ToDouble(list[3]), Convert.ToDouble(list[4]), endPoint2.Z);
                    XYZ endPoint1 = new XYZ(Convert.ToDouble(list[5]), Convert.ToDouble(list[6]), endPoint3.Z);
                    Line newWallLine = Line.CreateBound(endPoint0, endPoint1);
                    wallLine.Curve = newWallLine;
                }
                
                if (line.StartsWith("D;"))
                {
                    FamilyInstance door = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Doors)
                                           where element.Id.ToString() == list[1] select element).Cast<FamilyInstance>().ToList<FamilyInstance>().First();
                    FamilySymbol door_type = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_Doors)
                                              where element.Name == list[2] select element).Cast<FamilySymbol>().ToList<FamilySymbol>().First();
                    door.Symbol = door_type;
                    XYZ point = new XYZ(Convert.ToDouble(list[3]), Convert.ToDouble(list[4]), Convert.ToDouble(list[5]));
                    LocationPoint lp = door.Location as LocationPoint;
                    lp.Point = point;
                }

                if (line.StartsWith("F;"))
                {
                    FamilyInstance furniture = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Furniture)
                                                where element.Id.ToString() == list[1] select element).Cast<FamilyInstance>().ToList<FamilyInstance>().First();
                    FamilySymbol furniture_type = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_Furniture)
                                                   where element.Name == list[2] select element).Cast<FamilySymbol>().ToList<FamilySymbol>().First();
                    furniture.Symbol = furniture_type;
                }

                if (line.StartsWith("P;"))
                {
                    FamilyInstance plumbing = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_PlumbingFixtures)
                                                        where element.Id.ToString() == list[1] select element).Cast<FamilyInstance>().ToList<FamilyInstance>().First();
                    FamilySymbol plumbing_type = (from element in new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_PlumbingFixtures)
                                                           where element.Name == list[2] select element).Cast<FamilySymbol>().ToList<FamilySymbol>().First();
                    plumbing.Symbol = plumbing_type;
                }

                if (line.StartsWith("R;"))
                {
                    Room r = (from element in new FilteredElementCollector(doc).OfClass(typeof(SpatialElement)).OfCategory(BuiltInCategory.OST_Rooms)
                              where element.Id.ToString() == list[1] select element).Cast<Room>().ToList<Room>().First();
                    Parameter name = r.get_Parameter(BuiltInParameter.ROOM_NAME);
                    Parameter number = r.get_Parameter(BuiltInParameter.ROOM_NUMBER);
                    Parameter floor = r.get_Parameter(BuiltInParameter.ROOM_FINISH_FLOOR);
                    name.Set(list[2]);
                    number.Set(list[3]);
                    floor.Set(list[4]);
                    LocationPoint lp = r.Location as LocationPoint;
                    XYZ previousLocation = new XYZ(Convert.ToDouble(list[5]), Convert.ToDouble(list[6]), lp.Point.Z);
                    lp.Point = previousLocation;
                }
                counter++;
            }
            file.Close();
            }
            t.Commit();
        }

        return Result.Succeeded;
    }
}
}
