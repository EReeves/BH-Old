//Obsolete

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entity;
using ProtoBuf;
using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace Client
{
    class Level
    {
        public Dictionary<ObjectType,List<BaseEntity>> EntityDictionary = new Dictionary<ObjectType,List<BaseEntity>>();

        public Level()
        {
            string[] enums = Enum.GetNames(typeof(ObjectType));
            for (int i = 0; i < enums.Count(); i++)
            {
                List<BaseEntity> tempList = new List<BaseEntity>();
                ObjectType tempType;
                Enum.TryParse(enums[i], out tempType);
                EntityDictionary.Add(tempType, tempList);
            }
            Client.OnDraw += Draw;
        }

        public enum ObjectType
        {
            NoDraw,
            Draw,
            Static,
            Dynamic,
            NPC
        }
        
        public void Load(string mapname)
        {
            try
            {
                SaveDictionary dictionary;
                using (var file = File.OpenRead("Content/Maps/"+mapname))
                {
                    dictionary = Serializer.Deserialize<SaveDictionary>(file);
                }
                
                foreach (KeyValuePair<string, List<SaveObject>> pair in dictionary.dictionary)
                {
                        ObjectType tempType;
                        Enum.TryParse<ObjectType>(pair.Key, out tempType);

                        switch (tempType)
                        {
                            case ObjectType.Draw:
                                foreach (SaveObject so in pair.Value)
                                {
                                    BaseEntity tempEntity = new BaseEntity();
                                    tempEntity.UID = so.UID;
                                    tempEntity.Position = new Vector2f(so.x, so.y);
                                    tempEntity.Texture = new Texture("Content/Sprites/" + so.texture);
                                    tempEntity.Body.CollidesWith = FarseerPhysics.Dynamics.Category.None;
                                    EntityDictionary[tempType].Add(tempEntity);
                                }
                                break;

                            case ObjectType.NoDraw:
                                //No point in loading these.

                            case ObjectType.Static:
                                foreach (SaveObject so in pair.Value)
                                {
                                    BaseEntity tempEntity = new BaseEntity();
                                    tempEntity.UID = so.UID;
                                    tempEntity.Position = new Vector2f(so.x, so.y);
                                    tempEntity.Texture = new Texture("Content/Sprites/" + so.texture);
                                    tempEntity.SetRectangleBody((int)tempEntity.Texture.Size.X, (int)tempEntity.Texture.Size.Y);
                                    tempEntity.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
                                    if (so.Density != null)
                                        tempEntity.Body.Mass = Convert.ToInt32(so.Density);
                                    EntityDictionary[tempType].Add(tempEntity);
                                }
                                    break;

                            case ObjectType.Dynamic:
                                foreach (SaveObject so in pair.Value)
                                {
                                    BaseEntity tempEntity = new BaseEntity();
                                    tempEntity.UID = so.UID;
                                    tempEntity.Position = new Vector2f(so.x, so.y);
                                    tempEntity.Texture = new Texture("Content/Sprites/" + so.texture);
                                    tempEntity.SetRectangleBody((int)tempEntity.Texture.Size.X, (int)tempEntity.Texture.Size.Y);
                                    tempEntity.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
                                    if (so.Density != null)
                                        tempEntity.Body.Mass = Convert.ToInt32(so.Density);
                                    EntityDictionary[tempType].Add(tempEntity);
                                }
                                     break;

                            case ObjectType.NPC:
                                //Not implemented.
                                break;
                        }
                }
            }
            catch
            {
                return;
            }
        }

        public void Draw()
        {
            foreach (KeyValuePair<ObjectType, List<BaseEntity>> pair in EntityDictionary)
            {
                foreach (BaseEntity be in pair.Value)
                {
                    be.Draw(Client.Window, RenderStates.Default);
                }
            }
        }
    }


    [ProtoContract]
    class SaveObject
    {
        [ProtoMember(1)]
        public int UID { get; set; }
        [ProtoMember(2)]
        public string type { get; set; }
        [ProtoMember(3)]
        public int x { get; set; }
        [ProtoMember(4)]
        public int y { get; set; }
        [ProtoMember(5)]
        public string texture { get; set; }
        //Optional and type specific below.
        [ProtoMember(6)]
        public string Dialogue { get; set; }
        [ProtoMember(7)]
        public string NPCName { get; set; }
        [ProtoMember(8)]
        public string Density { get; set; }
    }

   /// <summary>
   /// Hacky class, lets the dictionary be serialized, I could inherit dictionary but this is easier.
   /// </summary>
    [ProtoContract]
    class SaveDictionary
    {
        [ProtoMember(1)]
        public Dictionary<string, List<SaveObject>> dictionary = new Dictionary<string, List<SaveObject>>();
    }
}
*/