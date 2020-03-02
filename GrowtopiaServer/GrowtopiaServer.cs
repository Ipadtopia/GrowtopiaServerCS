/**********************************************************************************
 * 
 *                    PROJECT REBORN IN C#
 *                    HADI, CMD , SECRET !
 * 
**********************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using ENet.Managed;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Collections;

namespace GrowtopiaServer
{
    class GrowtopiaServer
    {
        //Mysql Setup
        private const String MQSERVER = "127.0.0.1";
        private const String DATABASE = "gtdb";
        private const String UID = "root";
        private const String MQPASSWORD = "wfh13102003";
        private static MySqlConnection dbConn;
        //Mysql Setup
        public static ENetHost server;
        public static int cId = 1;
        public static byte[] itemsDat;
        public static int itemsDatSize = 0;
        public static List<ENetPeer> peers = new List<ENetPeer>();
        public static WorldDB worldDB;
        public static ItemDefinition[] itemDefs = new ItemDefinition[] { };
        public DroppedItem[] droppedItems = new DroppedItem[] { };
        public static Admin[] admins = new Admin[] { };
        public static void IntializeDB()
        {
            try
            {
                string connString = $"Server={MQSERVER};Database={DATABASE};Uid={UID};Pwd={MQPASSWORD};";

                dbConn = new MySqlConnection(connString);
                dbConn.Open();
            }
            catch
            {
                Console.WriteLine("error in open mysql connection");
            }
        }

        public static void updatedb(ENetPeer peer)
        {
            try
            {
                string growid = (peer.Data as PlayerInfo).rawName;
                string query = $"SELECT * FROM playerdb WHERE growid='{growid}';";
                MySqlCommand hadi = new MySqlCommand(query, dbConn);
                MySqlDataReader reader = hadi.ExecuteReader();
                if (reader.Read())
                {
                    string ai = (reader["accountID"].ToString());
                    string passo = (reader["password"].ToString());
                    string eemail = (reader["email"].ToString());
                    string disc = (reader["discord"].ToString());
                    int accountID = Convert.ToInt32(ai);
                    string nt = "";
                    int al = (peer.Data as PlayerInfo).adminLevel;
                    int cb = (peer.Data as PlayerInfo).cloth_back;
                    int ch = (peer.Data as PlayerInfo).cloth_hand;
                    int cf = (peer.Data as PlayerInfo).cloth_face;
                    int cs = (peer.Data as PlayerInfo).cloth_shirt;
                    int cp = (peer.Data as PlayerInfo).cloth_pants;
                    int cn = (peer.Data as PlayerInfo).cloth_neck;
                    int cha = (peer.Data as PlayerInfo).cloth_hair;
                    int cfe = (peer.Data as PlayerInfo).cloth_feet;
                    int cm = (peer.Data as PlayerInfo).cloth_mask;
                    int ca = (peer.Data as PlayerInfo).cloth_ances;
                    int a1 = (peer.Data as PlayerInfo).allow1;
                    int a2 = (peer.Data as PlayerInfo).allow2;
                    int a3 = (peer.Data as PlayerInfo).allow3;
                    int a4 = (peer.Data as PlayerInfo).allow4;
                    int a5 = (peer.Data as PlayerInfo).allow5;
                    int a6 = (peer.Data as PlayerInfo).allow6;
                    int a7 = (peer.Data as PlayerInfo).allow7;
                    int ll = (peer.Data as PlayerInfo).level;
                    int cl = (peer.Data as PlayerInfo).canleave;
                    int im = (peer.Data as PlayerInfo).isMuted;
                    int ib = (peer.Data as PlayerInfo).isBanned;
                    int ep = (peer.Data as PlayerInfo).exp;
                    int gm = (peer.Data as PlayerInfo).gem;
                    MySqlCommand cmd = new MySqlCommand("newacc", dbConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_accountID", accountID);
                    cmd.Parameters.AddWithValue("_growid", growid);
                    cmd.Parameters.AddWithValue("_password", passo);
                    cmd.Parameters.AddWithValue("_email", eemail);
                    cmd.Parameters.AddWithValue("_discord", disc);
                    cmd.Parameters.AddWithValue("_adminLevel", al);
                    cmd.Parameters.AddWithValue("_ClothBack", cb);
                    cmd.Parameters.AddWithValue("_ClothHand", ch);
                    cmd.Parameters.AddWithValue("_ClothFace", cf);
                    cmd.Parameters.AddWithValue("_ClothShirt", cs);
                    cmd.Parameters.AddWithValue("_ClothPants", cp);
                    cmd.Parameters.AddWithValue("_ClothNeck", cn);
                    cmd.Parameters.AddWithValue("_ClothHair", cha);
                    cmd.Parameters.AddWithValue("_ClothFeet", cfe);
                    cmd.Parameters.AddWithValue("_ClothMask", cm);
                    cmd.Parameters.AddWithValue("_ClothAnces", ca);
                    cmd.Parameters.AddWithValue("_allow1", a1);
                    cmd.Parameters.AddWithValue("_allow2", a2);
                    cmd.Parameters.AddWithValue("_allow3", a3);
                    cmd.Parameters.AddWithValue("_allow4", a4);
                    cmd.Parameters.AddWithValue("_allow5", a5);
                    cmd.Parameters.AddWithValue("_allow6", a6);
                    cmd.Parameters.AddWithValue("_allow7", a7);
                    cmd.Parameters.AddWithValue("_Level", ll);
                    cmd.Parameters.AddWithValue("_canleave", cl);
                    cmd.Parameters.AddWithValue("_isMuted", im);
                    cmd.Parameters.AddWithValue("_exp", ep);
                    cmd.Parameters.AddWithValue("_isBanned", ib);
                    cmd.Parameters.AddWithValue("_friends", nt);
                    cmd.Parameters.AddWithValue("_gem", gm);
                    reader.Close();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                }
            }
            catch
            {
                Console.WriteLine("error in update database");
            }
        }

        public static void updatecloth(ENetPeer peer)
        {
            try
            {
                string growid = (peer.Data as PlayerInfo).rawName;
                string query = $"SELECT * FROM playerdb WHERE growid='{growid}';";
                MySqlCommand hadi = new MySqlCommand(query, dbConn);
                MySqlDataReader reader = hadi.ExecuteReader();
                if (reader.Read())
                {
                    string al = (reader["adminLevel"].ToString());
                    string cb = (reader["ClothBack"].ToString());
                    string chn = (reader["ClothHand"].ToString());
                    string cf = (reader["ClothFace"].ToString());
                    string cs = (reader["ClothShirt"].ToString());
                    string cp = (reader["ClothPants"].ToString());
                    string cn = (reader["ClothNeck"].ToString());
                    string ch = (reader["ClothHair"].ToString());
                    string cfe = (reader["ClothFeet"].ToString());
                    string cm = (reader["ClothMask"].ToString());
                    string ca = (reader["ClothAnces"].ToString());
                    string a1 = (reader["allow1"].ToString());
                    string a2 = (reader["allow2"].ToString());
                    string a3 = (reader["allow3"].ToString());
                    string a4 = (reader["allow4"].ToString());
                    string a5 = (reader["allow5"].ToString());
                    string a6 = (reader["allow6"].ToString());
                    string a7 = (reader["allow7"].ToString());
                    string ll = (reader["Level"].ToString());
                    string cl = (reader["canleave"].ToString());
                    string im = (reader["isMuted"].ToString());
                    string ep = (reader["exp"].ToString());
                    string ib = (reader["isBanned"].ToString());
                    string gm = (reader["gem"].ToString());
                    (peer.Data as PlayerInfo).adminLevel = Convert.ToInt32(al);
                    (peer.Data as PlayerInfo).cloth_back = Convert.ToInt32(cb);
                    (peer.Data as PlayerInfo).cloth_hand = Convert.ToInt32(chn);
                    (peer.Data as PlayerInfo).cloth_face = Convert.ToInt32(cf);
                    (peer.Data as PlayerInfo).cloth_shirt = Convert.ToInt32(cs);
                    (peer.Data as PlayerInfo).cloth_pants = Convert.ToInt32(cp);
                    (peer.Data as PlayerInfo).cloth_neck = Convert.ToInt32(cn);
                    (peer.Data as PlayerInfo).cloth_hair = Convert.ToInt32(ch);
                    (peer.Data as PlayerInfo).cloth_feet = Convert.ToInt32(cfe);
                    (peer.Data as PlayerInfo).cloth_mask = Convert.ToInt32(cm);
                    (peer.Data as PlayerInfo).cloth_ances = Convert.ToInt32(ca);
                    (peer.Data as PlayerInfo).allow1 = Convert.ToInt32(a1);
                    (peer.Data as PlayerInfo).allow2 = Convert.ToInt32(a2);
                    (peer.Data as PlayerInfo).allow3 = Convert.ToInt32(a3);
                    (peer.Data as PlayerInfo).allow4 = Convert.ToInt32(a4);
                    (peer.Data as PlayerInfo).allow5 = Convert.ToInt32(a5);
                    (peer.Data as PlayerInfo).allow6 = Convert.ToInt32(a6);
                    (peer.Data as PlayerInfo).allow7 = Convert.ToInt32(a7);
                    (peer.Data as PlayerInfo).level = Convert.ToInt32(ll);
                    (peer.Data as PlayerInfo).canleave = Convert.ToInt32(cl);
                    (peer.Data as PlayerInfo).isMuted = Convert.ToInt32(im);
                    (peer.Data as PlayerInfo).exp = Convert.ToInt32(ep);
                    (peer.Data as PlayerInfo).isBanned = Convert.ToInt32(ib);
                    (peer.Data as PlayerInfo).gem = Convert.ToInt32(gm);
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
            }
            catch
            {
                Console.WriteLine("error in updating clothes");
            }
        }
        public static byte[] FromHex(string hex)
        {

            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static void sendData(ENetPeer peer, int num, byte[] data, int len)
        {
            try
            {
                byte[] packet = new byte[len + 5];
                Array.Copy(BitConverter.GetBytes(num), 0, packet, 0, 4);
                if (data != null)
                {
                    Array.Copy(data, 0, packet, 4, len);
                }

                packet[4 + len] = 0;
                peer.Send(packet, 0, ENetPacketFlags.Reliable);
                server.Flush();
            }
            catch
            {
                Console.WriteLine("Error in void sendData");
            }
        }

        public int getPacketId(byte[] data)
        {
            return data[0];
        }

        public byte[] getPacketData(byte[] data)
        {
            return data.Skip(4).ToArray();
        }

        public string text_encode(string text)
        {
            string ret = "";
            int i = 0;
            while (text[i] != 0)
            {
                switch (text[i])
                {
                    case '\n':
                        ret += "\\n";
                        break;
                    case '\t':
                        ret += "\\t";
                        break;
                    case '\b':
                        ret += "\\b";
                        break;
                    case '\\':
                        ret += "\\\\";
                        break;
                    case '\r':
                        ret += "\\r";
                        break;
                    default:
                        ret += text[i];
                        break;
                }

                i++;
            }

            return ret;
        }

        public static byte ch2n(char x)
        {
            switch (x)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
            }

            return 0;
        }

        public static string[] explode(string delimiter, string str)
        {
            return str.Split(delimiter.ToCharArray());
        }

        public static string DecimalToHexadecimal(int dec)
        {
            int hex = dec;
            string hexStr = string.Empty;

            while (dec > 0)
            {
                hex = dec % 16;

                if (hex < 10)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                dec /= 16;
            }

            return hexStr;
        }
        public struct GamePacket
        {
            public byte[] data;
            public int len;
            public int indexes;
        }

        public static GamePacket appendFloat(GamePacket p, float val)
        {
            byte[] data = new byte[p.len + 2 + 4];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] num = BitConverter.GetBytes(val);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 1;
            Array.Copy(num, 0, data, p.len + 2, 4);
            p.len = p.len + 2 + 4;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket appendFloat(GamePacket p, float val, float val2)
        {
            byte[] data = new byte[p.len + 2 + 8];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] fl1 = BitConverter.GetBytes(val);
            byte[] fl2 = BitConverter.GetBytes(val2);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 3;
            Array.Copy(fl1, 0, data, p.len + 2, 4);
            Array.Copy(fl2, 0, data, p.len + 6, 4);
            p.len = p.len + 2 + 8;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket appendFloat(GamePacket p, float val, float val2, float val3)
        {
            byte[] data = new byte[p.len + 2 + 12];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] fl1 = BitConverter.GetBytes(val);
            byte[] fl2 = BitConverter.GetBytes(val2);
            byte[] fl3 = BitConverter.GetBytes(val3);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 4;
            Array.Copy(fl1, 0, data, p.len + 2, 4);
            Array.Copy(fl2, 0, data, p.len + 6, 4);
            Array.Copy(fl3, 0, data, p.len + 10, 4);
            p.len = p.len + 2 + 12;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket appendInt(GamePacket p, Int32 val)
        {
            byte[] data = new byte[p.len + 2 + 4];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] num = BitConverter.GetBytes(val);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 9;
            Array.Copy(num, 0, data, p.len + 2, 4);
            p.len = p.len + 2 + 4;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket appendIntx(GamePacket p, Int32 val)
        {
            byte[] data = new byte[p.len + 2 + 4];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] num = BitConverter.GetBytes(val);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 5;
            Array.Copy(num, 0, data, p.len + 2, 4);
            p.len = p.len + 2 + 4;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket appendString(GamePacket p, string str)
        {
            byte[] data = new byte[p.len + 2 + str.Length + 4];
            Array.Copy(p.data, 0, data, 0, p.len);
            byte[] strn = Encoding.ASCII.GetBytes(str);
            data[p.len] = (byte)p.indexes;
            data[p.len + 1] = 2;
            byte[] len = BitConverter.GetBytes(str.Length);
            Array.Copy(len, 0, data, p.len + 2, 4);
            Array.Copy(strn, 0, data, p.len + 6, str.Length);
            p.len = p.len + 2 + str.Length + 4;
            p.indexes++;
            p.data = data;
            return p;
        }

        public static GamePacket createPacket()
        {
            byte[] data = new byte[61];
            string asdf = "0400000001000000FFFFFFFF00000000080000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            for (int i = 0; i < asdf.Length; i += 2)
            {
                byte x = ch2n(asdf[i]);
                x = (byte)(x << 4);
                x += ch2n(asdf[i + 1]);
                data[i / 2] = x;
                if (asdf.Length > 61 * 2) throw new Exception("?");
            }
            GamePacket packet;
            packet.data = data;
            packet.len = 61;
            packet.indexes = 0;
            return packet;
        }

        public static GamePacket packetEnd(GamePacket p)
        {
            byte[] n = new byte[p.len + 1];
            Array.Copy(p.data, 0, n, 0, p.len);
            p.data = n;
            p.data[p.len] = 0;
            p.len += 1;
            p.data[56] = (byte)p.indexes;
            p.data[60] = (byte)p.indexes;
            //*(BYTE*)(p.data + 60) = p.indexes;
            return p;
        }

        public struct InventoryItem
        {
            public Int16 itemID;
            public byte itemCount;
        }

        public class PlayerInventory
        {
            public InventoryItem[] items = new InventoryItem[] { };
            public int inventorySize = 100;
        };

        public class PlayerInfo
        {
            public bool isIn = false;
            public int netID;
            public bool haveGrowId = false;
            public string tankIDName = "";
            public string tankIDPass = "";
            public string requestedName = "";
            public string rawName = "";
            public string displayName = "";
            public string country = "";
            public string lastmsger = "";
            public string lastmsgerworld = "";
            public string wk = "";
            public string rid = "";
            public string gid = "";
            public string aid = "";
            public string vid = "";
            public string zf = "";
            public string gameverison = "";
            public string platformID = "";
            public string mac = "";
            public int adminLevel = 0;
            public string currentWorld = "EXIT";
            public bool radio = true;
            public int x;
            public int y;
            public int x1;
            public int y1;
            public bool isRotatedLeft = false;

            public bool isUpdating = false;
            public bool joinClothesUpdated = false;

            public int cloth_hair = 0; // 0
            public int cloth_shirt = 0; // 1
            public int cloth_pants = 0; // 2
            public int cloth_feet = 0; // 3
            public int cloth_face = 0; // 4
            public int cloth_hand = 0; // 5
            public int cloth_back = 0; // 6
            public int cloth_mask = 0; // 7
            public int cloth_necklace = 0; // 8
            public int cloth_ances = 0;
            public int cloth_neck = 0;
            public int dropid = 0;
            public int effect = 0;

            public int allow1 = 0;
            public int allow2 = 0;
            public int allow3 = 0;
            public int allow4 = 0;
            public int allow5 = 0;
            public int allow6 = 0;
            public int allow7 = 0;

            public int isBanned = 0;
            public int canleave = 0;
            public int isMuted = 0;
            public int level = 0;
            public int exp = 0;
            public int gem = 0;
            public int cpX;
            public int cpY;
            public int log = 0;

            public bool istrading = false;
            public int item1 = 0;
            public int item1count = 0;
            public int item2 = 0;
            public int item2count = 0;
            public int item3 = 0;
            public int item3count = 0;
            public int item4 = 0;
            public int item4count = 0;
            public bool accepted = false;
            public string tradingme = "";
            public bool dotrade = false;

            public int lastPunchX = 0;
            public int lastPunchY = 0;
            public int droppeditemcount = 0;

            public bool canWalkInBlocks = false; // 1
            public bool canDoubleJump = false; // 2
            public bool isInvisible = false; // 4
            public bool noHands = false; // 8
            public bool noEyes = false; // 16
            public bool noBody = false; // 32
            public bool devilHorns = false; // 64
            public bool goldenHalo = false; // 128
            public bool isFrozen = false; // 2048
            public bool isCursed = false; // 4096
            public bool isDuctaped = false; // 8192
            public bool haveCigar = false; // 16384
            public bool isShining = false; // 32768
            public bool isZombie = false; // 65536
            public bool isHitByLava = false; // 131072
            public bool haveHauntedShadows = false; // 262144
            public bool haveGeigerRadiation = false; // 524288
            public bool haveReflector = false; // 1048576
            public bool isEgged = false; // 2097152
            public bool havePineappleFloag = false; // 4194304
            public bool haveFlyingPineapple = false; // 8388608
            public bool haveSuperSupporterName = false; // 16777216
            public bool haveSupperPineapple = false; // 33554432
                                                     //bool 
            public uint skinColor = 0x8295C3FF; //normal SKin color like gt!
            public bool isinv = false;

            public PlayerInventory inventory = new PlayerInventory();

            public long lastSB = 0;
            public long cansave = 0;
            public long lastjoinreq = 0;
            public long lastent = 0;
            public long lastent1 = 0;
        }


        public static int getState(PlayerInfo info)
        {
            int val = 0;
            val |= info.canWalkInBlocks ? 1 : 0 << 0;
            val |= info.canDoubleJump ? 1 : 0 << 1;
            val |= info.isInvisible ? 1 : 0 << 2;
            val |= info.noHands ? 1 : 0 << 3;
            val |= info.noEyes ? 1 : 0 << 4;
            val |= info.noBody ? 1 : 0 << 5;
            val |= info.devilHorns ? 1 : 0 << 6;
            val |= info.goldenHalo ? 1 : 0 << 7;
            return val;
        }

        public static void sendconsolemsg(ENetPeer peer, string x)
        {
            try
            {
                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), x));
                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in void sendconsolemsg");
            }
        }

        public struct WorldItem
        {
            public Int16 foreground;
            public Int16 background;
            public int breakLevel;
            public long breakTime;
            public bool water;
            public bool fire;
            public bool glue;
            public bool red;
            public bool green;
            public bool blue;
            public int gravity;
            public int stuffwe;
            public int dblock;
            public string sign;
            public int usedsign;
            public int useddoor;
            public string dtext;
            public string dest;
            public string did;
            public int iop;
            public bool sold;
            public int invend;
            public int price;
            public string drop;
            public int useddrop;
            public int uid;
        };

        public class WorldInfo
        {
            public int width = 100;
            public int height = 60;
            public string name = "TEST";
            public WorldItem[] items;
            public string owner = "";
            public bool isPublic = false;
            public bool isNuked = false;
            public int weather = 0;
            public string access = "";
            public int dropcount = 0;
        };

        public static WorldInfo generateWorld(string name, int width, int height)
        {
            WorldInfo world = new WorldInfo();
            Random rand = new Random();
            world.name = name;
            world.width = width;
            world.height = height;
            world.items = new WorldItem[world.width * world.height];
            for (int i = 0; i < world.width * world.height; i++)
            {
                if (i >= 3800 && i < 5400 && rand.Next(0, 50) == 0)
                    world.items[i].foreground = 10;
                else if (i >= 3700 && i < 5400)
                {
                    world.items[i].foreground = 2;
                }
                else if (i >= 5400)
                {
                    world.items[i].foreground = 8;
                }
                if (i >= 3700)
                    world.items[i].background = 14;
                if (i == 3650)
                    world.items[i].foreground = 6;
                else if (i >= 3600 && i < 3700)
                    world.items[i].foreground = 0; //fixed the grass in the world!
                if (i == 3750)
                    world.items[i].foreground = 8;
            }
            return world;
        }

        public class PlayerDB
        {
            public static string getProperName(string name)
            {
                string newS = name.ToLower();
                var ret = new StringBuilder();
                for (int i = 0; i < newS.Length; i++)
                {
                    if (newS[i] == '`') i++; else ret.Append(newS[i]);
                }

                var ret2 = new StringBuilder();
                foreach (char c in ret.ToString()) if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')) ret2.Append(c);
                return ret2.ToString();
            }

            public static string fixColors(string text)
            {
                string ret = "";
                int colorLevel = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '`')
                    {
                        ret += text[i];
                        if (i + 1 < text.Length)
                            ret += text[i + 1];


                        if (i + 1 < text.Length && text[i + 1] == '`')
                        {
                            colorLevel--;
                        }
                        else
                        {
                            colorLevel++;
                        }
                        i++;
                    }
                    else
                    {
                        ret += text[i];
                    }
                }
                for (int i = 0; i < colorLevel; i++)
                {
                    ret += "``";
                }
                for (int i = 0; i > colorLevel; i--)
                {
                    ret += "`w";
                }
                return ret;
            }

            public static int playerLogin(ENetPeer peer, string username, string password)
            {
                string query = $"SELECT * FROM playerdb WHERE growid='{username}' AND password='{password}';";
                MySqlCommand hadi = new MySqlCommand(query, dbConn);
                MySqlDataReader reader = hadi.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    foreach (ENetPeer currentPeer in peers)
                    {
                        if (currentPeer.State != ENetPeerState.Connected)
                            continue;
                        if (currentPeer == peer)
                            continue;
                        if ((currentPeer.Data as PlayerInfo).rawName == getProperName(username))
                        {

                            {
                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "Someone else logged into this account!"));
                                currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                            }
                            {
                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "Someone else was logged into this account! He was kicked out now."));
                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                            }
                            //enet_host_flush(server);
                            currentPeer.DisconnectLater(0);
                        }
                    }
                    return 1;
                }
                else
                {
                    reader.Close();
                    return -1;
                }
            }

            public static int playerRegister(string username, string password, string passwordverify, string email, string discord)
            {
                username = getProperName(username);
                if (username.Contains("@") || username.Contains("#") || username.Contains("!") || username.Contains("`") || username.Contains("$") || username.Contains("%") || username.Contains("^")
                    || username.Contains("&") || username.Contains("*") || username.Contains("(") || username.Contains(")") || username.Contains("-") || username.Contains("_") || username.Contains("+")
                     || username.Contains("[") || username.Contains("]") || username.Contains("|") || username.Contains("'") || username.Contains(",") || username.Contains(":") || username.Contains(";")
                      || username.Contains(" ") || username.Contains(".") || username.Contains("="))
                {
                    return -7;
                }
                string name = username;
                if (name == "CON" || name == "PRN" || name == "AUX" || name == "NUL" || name == "COM1" || name == "COM2" || name == "COM3" || name == "COM4" || name == "COM5" || name == "COM6" || name == "COM7" || name == "COM8" || name == "COM9" || name == "LPT1" || name == "LPT2" || name == "LPT3" || name == "LPT4" || name == "LPT5" || name == "LPT6" || name == "LPT7" || name == "LPT8" || name == "LPT9" || name == "con" || name == "prn" || name == "aux" || name == "nul" || name == "com1" || name == "com2" || name == "com3" || name == "com4" || name == "com5" || name == "com6" || name == "com7" || name == "com8" || name == "com9" || name == "lpt1" || name == "lpt2" || name == "lpt3" || name == "lpt4" || name == "lpt5" || name == "lpt6" || name == "lpt7" || name == "lpt8" || name == "lpt9")
                {
                    return -8;
                }
                if (!discord.Contains("#") && discord.Length != 0) return -5;
                if (!email.Contains("@") && email.Length != 0) return -4;
                if (passwordverify != password) return -3;
                if (username.Length < 3) return -2;
                int accountID = 0;
                int zero = 0;
                string nt = "";
                String growid = username;
                String pass = password;
                String eemail = email;
                String disc = discord;

                MySqlCommand check_User_Name = new MySqlCommand("SELECT * FROM playerdb WHERE (growid = @user)", dbConn);
                check_User_Name.Parameters.AddWithValue("@user", username);
                MySqlDataReader reader = check_User_Name.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return -1;
                }
                else
                {
                    reader.Close();
                    MySqlCommand cmd = new MySqlCommand("newacc", dbConn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_accountID", accountID);
                    cmd.Parameters.AddWithValue("_growid", growid);
                    cmd.Parameters.AddWithValue("_password", pass);
                    cmd.Parameters.AddWithValue("_email", eemail);
                    cmd.Parameters.AddWithValue("_discord", disc);
                    cmd.Parameters.AddWithValue("_adminLevel", zero);
                    cmd.Parameters.AddWithValue("_ClothBack", zero);
                    cmd.Parameters.AddWithValue("_ClothHand", zero);
                    cmd.Parameters.AddWithValue("_ClothFace", zero);
                    cmd.Parameters.AddWithValue("_ClothShirt", zero);
                    cmd.Parameters.AddWithValue("_ClothPants", zero);
                    cmd.Parameters.AddWithValue("_ClothNeck", zero);
                    cmd.Parameters.AddWithValue("_ClothHair", zero);
                    cmd.Parameters.AddWithValue("_ClothFeet", zero);
                    cmd.Parameters.AddWithValue("_ClothMask", zero);
                    cmd.Parameters.AddWithValue("_ClothAnces", zero);
                    cmd.Parameters.AddWithValue("_allow1", zero);
                    cmd.Parameters.AddWithValue("_allow2", zero);
                    cmd.Parameters.AddWithValue("_allow3", zero);
                    cmd.Parameters.AddWithValue("_allow4", zero);
                    cmd.Parameters.AddWithValue("_allow5", zero);
                    cmd.Parameters.AddWithValue("_allow6", zero);
                    cmd.Parameters.AddWithValue("_allow7", zero);
                    cmd.Parameters.AddWithValue("_Level", zero);
                    cmd.Parameters.AddWithValue("_canleave", zero);
                    cmd.Parameters.AddWithValue("_isMuted", zero);
                    cmd.Parameters.AddWithValue("_exp", zero);
                    cmd.Parameters.AddWithValue("_isBanned", zero);
                    cmd.Parameters.AddWithValue("_friends", nt);
                    cmd.Parameters.AddWithValue("_gem", zero);
                    cmd.ExecuteNonQuery();
                    return 1;
                }
            }
        }

        public struct AWorld
        {
            public WorldInfo info;
            public int id;
        };

        public class WorldDB
        {
            public WorldInfo[] worlds = new WorldInfo[] { };

            public AWorld get2(string name)
            {
                if (worlds.Length > 200)
                {
                    Console.WriteLine("Saving redundant worlds!");
                    saveRedundant();
                    Console.WriteLine("Redundant worlds are saved!");
                }

                AWorld ret = new AWorld();
                name = getStrUpper(name);
                if (name.Length < 1) throw new Exception("too short name"); // too short name
                foreach (char c in name)
                {
                    if ((c < 'A' || c > 'Z') && (c < '0' || c > '9'))
                        throw new Exception("bad name"); // wrong name
                }
                if (name == "EXIT")
                {
                    throw new Exception("exit world");
                }
                for (int i = 0; i < worlds.Length; i++)
                {
                    if (worlds[i].name == name)
                    {
                        ret.id = i;
                        ret.info = worlds[i];
                        return ret;
                    }
                }

                string path = "worlds/" + name + ".json";
                if (File.Exists(path))
                {
                    string contents = File.ReadAllText(path);
                    JObject j = JObject.Parse(contents);
                    WorldInfo info = new WorldInfo();
                    info.name = (string)j["name"];
                    info.width = (int)j["width"];
                    info.height = (int)j["height"];
                    info.owner = (string)j["owner"];
                    info.access = (string)j["access"];
                    info.dropcount = (int)j["dropcount"];
                    info.isPublic = (bool)j["isPublic"];
                    info.isNuked = (bool)j["isNuked"];
                    info.weather = (short)j["weather"];
                    JArray tiles = (JArray)j["tiles"];
                    int square = info.width * info.height;
                    info.items = new WorldItem[square];
                    for (int i = 0; i < square; i++)
                    {
                        info.items[i].foreground = (short)tiles[i]["fg"];
                        info.items[i].background = (short)tiles[i]["bg"];
                        info.items[i].stuffwe = (short)tiles[i]["sw"];
                        info.items[i].gravity = (short)tiles[i]["gr"];
                        info.items[i].dblock = (short)tiles[i]["db"];
                        info.items[i].sign = (string)tiles[i]["s"];
                        info.items[i].usedsign = (short)tiles[i]["us"];
                        info.items[i].useddoor = (short)tiles[i]["ud"];
                        info.items[i].dtext = (string)tiles[i]["dt"];
                        info.items[i].dest = (string)tiles[i]["ds"];
                        info.items[i].did = (string)tiles[i]["id"];
                        info.items[i].iop = (short)tiles[i]["op"];
                        info.items[i].sold = (bool)tiles[i]["sd"];
                        info.items[i].invend = (short)tiles[i]["iv"];
                        info.items[i].price = (short)tiles[i]["pr"];
                        info.items[i].drop = (string)tiles[i]["dp"];
                        info.items[i].useddrop = (short)tiles[i]["du"];
                        info.items[i].uid = (short)tiles[i]["ui"];
                    }

                    worlds = worlds.Append(info).ToArray();
                    ret.id = worlds.Length - 1;
                    ret.info = info;
                    return ret;
                }
                else
                {
                    WorldInfo info = generateWorld(name, 100, 60);
                    worlds = worlds.Append(info).ToArray();
                    ret.id = worlds.Length - 1;
                    ret.info = info;
                    return ret;
                }
            }

            public WorldInfo get(string name)
            {
                return get2(name).info;
            }

            public void flush(WorldInfo info)
            {
                string path = "worlds/" + info.name + ".json";
                JArray tiles = new JArray();
                int square = info.width * info.height;

                for (int i = 0; i < square; i++)
                {
                    JObject tile = new JObject(
                        new JProperty("fg", info.items[i].foreground),
                        new JProperty("bg", info.items[i].background),
                        new JProperty("sw", info.items[i].stuffwe),
                        new JProperty("gr", info.items[i].gravity),
                        new JProperty("db", info.items[i].dblock),
                        new JProperty("s", info.items[i].sign),
                        new JProperty("us", info.items[i].usedsign),
                        new JProperty("dt", info.items[i].dtext),
                        new JProperty("ud", info.items[i].useddoor),
                        new JProperty("ds", info.items[i].dest),
                        new JProperty("id", info.items[i].did),
                        new JProperty("op", info.items[i].iop),
                        new JProperty("sd", info.items[i].sold),
                        new JProperty("iv", info.items[i].invend),
                        new JProperty("pr", info.items[i].price),
                        new JProperty("dp", info.items[i].drop),
                        new JProperty("du", info.items[i].useddrop),
                        new JProperty("ui", info.items[i].uid)
                        );
                    tiles.Add(tile);
                }
                JObject j = new JObject(
                    new JProperty("name", info.name),
                    new JProperty("width", info.width),
                    new JProperty("height", info.height),
                    new JProperty("owner", info.owner),
                    new JProperty("access", info.access),
                    new JProperty("dropcount", info.dropcount),
                    new JProperty("isPublic", info.isPublic),
                    new JProperty("isNuked", info.isNuked),
                    new JProperty("weather", info.weather),
                    new JProperty("tiles", tiles)
                );
                File.WriteAllText(path, j.ToString());
            }

            public void flush2(AWorld info)
            {
                flush(info.info);
            }

            public void save(AWorld info)
            {
                flush2(info);
                Array.Clear(worlds, info.id, 1);
            }

            public void saveAll()
            {
                for (int i = 0; i < worlds.Length; i++)
                {
                    flush(worlds[i]);
                }
                worlds = new WorldInfo[] { };
            }

            public WorldInfo[] getRandomWorlds()
            {
                WorldInfo[] ret = new WorldInfo[] { };
                for (int i = 0; i < ((worlds.Length < 10) ? worlds.Length : 10); i++)
                { // load first four worlds, it is excepted that they are special
                    ret = ret.Append(worlds[i]).ToArray();
                }
                // and lets get up to 6 random
                if (worlds.Length > 4)
                {
                    Random rand = new Random();
                    for (int j = 0; j < 6; j++)
                    {
                        bool isPossible = true;
                        WorldInfo world = worlds[rand.Next(0, worlds.Length - 4)];
                        for (int i = 0; i < ret.Length; i++)
                        {
                            if (world.name == ret[i].name || world.name == "EXIT")
                            {
                                isPossible = false;
                            }
                        }
                        if (isPossible)
                            ret = ret.Append(world).ToArray();
                    }
                }
                return ret;
            }

            public void saveRedundant()
            {
                for (int i = 4; i < worlds.Length; i++)
                {
                    bool canBeFree = true;

                    foreach (ENetPeer currentPeer in peers)
                    {
                        if (currentPeer.State != ENetPeerState.Connected) continue;
                        if ((currentPeer.Data as PlayerInfo).currentWorld == worlds[i].name)
                            canBeFree = false;
                    }

                    if (canBeFree)
                    {
                        flush(worlds[i]);
                        Array.Clear(worlds, i, 1);
                        i--;
                    }
                }
            }
        }

        public static string getStrUpper(string txt)
        {
            string ret = "";
            foreach (char c in txt) ret += c.ToString().ToUpper();
            return ret;
        }

        public static void saveAllWorlds() // atexit hack plz fix
        {
            Console.WriteLine("Saving worlds...");
            worldDB.saveAll();
            Console.WriteLine("Worlds saved!");

        }

        public static WorldInfo getPlyersWorld(ENetPeer peer)
        {
            try
            {
                return worldDB.get2((peer.Data as PlayerInfo).currentWorld).info;
            }
            catch
            {
                return null;
            }
        }

        public struct PlayerMoving
        {
            public int packetType;
            public int netID;
            public float x;
            public float y;
            public int characterState;
            public int plantingTree;
            public float XSpeed;
            public float YSpeed;
            public int punchX;
            public int punchY;
        };


        public enum ClothTypes
        {
            HAIR,
            SHIRT,
            PANTS,
            FEET,
            FACE,
            HAND,
            BACK,
            MASK,
            NECKLACE,
            NONE
        };

        public enum BlockTypes
        {
            FOREGROUND,
            BACKGROUND,
            SEED,
            PAIN_BLOCK,
            BEDROCK,
            MAIN_DOOR,
            SIGN,
            DOOR,
            CLOTHING,
            FIST,
            UNKNOWN
        };

        public struct ItemDefinition
        {
            public int id;
            public string name;
            public int rarity;
            public int breakHits;
            public int growTime;
            public ClothTypes clothType;
            public BlockTypes blockType;
            public string description;
        }

        public struct DroppedItem
        { // TODO
            public int id;
            public int uid;
            public int count;
        };

        public static ItemDefinition getItemDef(int id)
        {
            if (id < itemDefs.Length && id > -1)
                return itemDefs[id];
            return itemDefs[0];
        }

        public struct Admin
        {
            public string username;
            public string password;
            public int level;
            public long lastSB;
        };

        public static void craftItemDescriptions()
        {
            if (!File.Exists("Descriptions.txt")) return;
            string contents = File.ReadAllText("Descriptions.txt");
            foreach (string line in contents.Split("\n".ToCharArray()))
            {
                if (line.Length > 3 && line[0] != '/' && line[1] != '/')
                {
                    string[] ex = explode("|", line);
                    if (Convert.ToInt32(ex[0]) + 1 < itemDefs.Length)
                    {
                        itemDefs[Convert.ToInt32(ex[0])].description = ex[1];
                        if ((Convert.ToInt32(ex[0]) % 2) == 0)
                            itemDefs[Convert.ToInt32(ex[0]) + 1].description = "This is a tree.";
                    }
                }
            }
        }

        public static void buildItemsDatabase()
        {
            int current = -1;
            if (!File.Exists("CoreData.txt")) return;
            string contents = File.ReadAllText("CoreData.txt");
            foreach (string line in contents.Split("\n".ToCharArray()))
            {
                if (line.Length > 8 && line[0] != '/' && line[1] != '/')
                {
                    string[] ex = explode("|", line);
                    ItemDefinition def = new ItemDefinition();
                    def.id = Convert.ToInt32(ex[0]);
                    def.name = ex[1];
                    def.rarity = Convert.ToInt32(ex[2]);
                    string bt = ex[4];
                    if (bt == "Foreground_Block")
                    {
                        def.blockType = BlockTypes.FOREGROUND;
                    }
                    else if (bt == "Seed")
                    {
                        def.blockType = BlockTypes.SEED;
                    }
                    else if (bt == "Pain_Block")
                    {
                        def.blockType = BlockTypes.PAIN_BLOCK;
                    }
                    else if (bt == "Main_Door")
                    {
                        def.blockType = BlockTypes.MAIN_DOOR;
                    }
                    else if (bt == "Bedrock")
                    {
                        def.blockType = BlockTypes.BEDROCK;
                    }
                    else if (bt == "Door")
                    {
                        def.blockType = BlockTypes.DOOR;
                    }
                    else if (bt == "Fist")
                    {
                        def.blockType = BlockTypes.FIST;
                    }
                    else if (bt == "Sign")
                    {
                        def.blockType = BlockTypes.SIGN;
                    }
                    else if (bt == "Background_Block")
                    {
                        def.blockType = BlockTypes.BACKGROUND;
                    }
                    else
                    {
                        def.blockType = BlockTypes.UNKNOWN;
                    }
                    def.breakHits = Convert.ToInt32(ex[7]);
                    def.growTime = Convert.ToInt32(ex[8]);
                    string cl = ex[9];
                    if (cl == "None")
                    {
                        def.clothType = ClothTypes.NONE;
                    }
                    else if (cl == "Hat")
                    {
                        def.clothType = ClothTypes.HAIR;
                    }
                    else if (cl == "Shirt")
                    {
                        def.clothType = ClothTypes.SHIRT;
                    }
                    else if (cl == "Pants")
                    {
                        def.clothType = ClothTypes.PANTS;
                    }
                    else if (cl == "Feet")
                    {
                        def.clothType = ClothTypes.FEET;
                    }
                    else if (cl == "Face")
                    {
                        def.clothType = ClothTypes.FACE;
                    }
                    else if (cl == "Hand")
                    {
                        def.clothType = ClothTypes.HAND;
                    }
                    else if (cl == "Back")
                    {
                        def.clothType = ClothTypes.BACK;
                    }
                    else if (cl == "Hair")
                    {
                        def.clothType = ClothTypes.MASK;
                    }
                    else if (cl == "Chest")
                    {
                        def.clothType = ClothTypes.NECKLACE;
                    }
                    else
                    {
                        def.clothType = ClothTypes.NONE;
                    }

                    if (++current != def.id)
                    {
                        Console.WriteLine("Critical error! Unordered database at item " + current + "/" + def.id);
                    }

                    itemDefs = itemDefs.Append(def).ToArray();
                }
            }
            craftItemDescriptions();
        }

        public void addAdmin(string username, string password, int level)
        {
            Admin admin = new Admin();
            admin.username = username;
            admin.password = password;
            admin.level = level;
            admins = admins.Append(admin).ToArray();
        }

        public static int getAdminLevel(string username, string password)
        {
            for (int i = 0; i < admins.Length; i++)
            {
                Admin admin = admins[i];
                if (admin.username == username && admin.password == password)
                {
                    return admin.level;
                }
            }
            return 0;
        }

        public static bool canSB(string username, string password)
        {
            for (int i = 0; i < admins.Length; i++)
            {
                Admin admin = admins[i];
                if (admin.username == username && admin.password == password && admin.level > 1)
                {
                    long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    if (admin.lastSB + 900000 < time || admin.level == 999)
                    {
                        admins[i].lastSB = time;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canClear(string username, string password)
        {
            for (int i = 0; i < admins.Length; i++)
            {
                Admin admin = admins[i];
                if (admin.username == username && admin.password == password)
                {
                    return admin.level > 0;
                }
            }
            return false;
        }

        public static bool isSuperAdmin(string username, string password)
        {
            for (int i = 0; i < admins.Length; i++)
            {
                Admin admin = admins[i];
                if (admin.username == username && admin.password == password && admin.level == 999)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isHere(ENetPeer peer, ENetPeer peer2)
        {
            return ((peer.Data as PlayerInfo).currentWorld == (peer2.Data as PlayerInfo).currentWorld);
        }

        public static void sendInventory(ENetPeer peer, PlayerInventory inventory)
        {
            try
            {
                string asdf2 = "0400000009A7379237BB2509E8E0EC04F8720B050000000000000000FBBB0000010000007D920100FDFDFDFD04000000040000000000000000000000000000000000";
                int inventoryLen = inventory.items.Length;
                int packetLen = (asdf2.Length / 2) + (inventoryLen * 4) + 4;
                byte[] data2 = new byte[packetLen];
                for (int i = 0; i < asdf2.Length; i += 2)
                {
                    byte x = ch2n(asdf2[i]);
                    x = (byte)(x << 4);
                    x += ch2n(asdf2[i + 1]);
                    data2[i / 2] = x;
                }
                byte[] endianInvVal = BitConverter.GetBytes(inventoryLen);
                Array.Reverse(endianInvVal);
                Array.Copy(endianInvVal, 0, data2, asdf2.Length / 2 - 4, 4);
                endianInvVal = BitConverter.GetBytes(inventory.inventorySize);
                Array.Reverse(endianInvVal);
                Array.Copy(endianInvVal, 0, data2, asdf2.Length / 2 - 8, 4);
                int val = 0;
                for (int i = 0; i < inventoryLen; i++)
                {
                    val = 0;
                    val |= inventory.items[i].itemID;
                    val |= inventory.items[i].itemCount << 16;
                    val &= 0x00FFFFFF;
                    val |= 0x00 << 24;
                    byte[] value = BitConverter.GetBytes(val);
                    Array.Copy(value, 0, data2, asdf2.Length / 2 + (i * 4), 4);
                }

                peer.Send(data2, 0, ENetPacketFlags.Reliable);
                //enet_host_flush(server);
            }
            catch
            {
                Console.WriteLine("error in void sendinventory");
            }
        }

        public struct TileExtra
        {
            public int packetType;
            public int characterState;
            public float objectSpeedX;
            public int punchX;
            public int punchY;
            public int charStat;
            public int blockid;
            public int visual;
            public int signs;
            public int backgroundid;
            public int displayblock;
            public int time;
            public int netID;
            public int weatherspeed;
            public int bpm;
            //int unused1;
            //int unused2;
            //int unused3;
            //int bpm;
        };

        public static byte[] packvisual(TileExtra dataStruct, int options, int gravity)
        {
            byte[] data = new byte[102];
            for (int i = 0; i < 102; i++)
            {
                data[i] = 0;
            }
            Array.Copy(BitConverter.GetBytes(dataStruct.packetType), 0, data, 0, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.netID), 0, data, 8, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.characterState), 0, data, 12, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.punchX), 0, data, 44, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.punchY), 0, data, 48, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.charStat), 0, data, 52, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.blockid), 0, data, 56, 2);
            Array.Copy(BitConverter.GetBytes(dataStruct.backgroundid), 0, data, 58, 2);
            Array.Copy(BitConverter.GetBytes(dataStruct.visual), 0, data, 60, 4);
            Array.Copy(BitConverter.GetBytes(dataStruct.displayblock), 0, data, 64, 4);
            Array.Copy(BitConverter.GetBytes(gravity), 0, data, 68, 4);
            Array.Copy(BitConverter.GetBytes(options), 0, data, 70, 4);
            return data;
        }
        public static byte[] packPlayerMoving(PlayerMoving dataStruct)
        {
            byte[] data = new byte[56];
            for (int i = 0; i < 56; i++)
            {
                data[i] = 0;
            }
            Array.Copy(BitConverter.GetBytes(dataStruct.packetType), 0, data, 0, 4);
            //Console.WriteLine("packetType: " + dataStruct.packetType.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.netID), 0, data, 4, 4);
            //Console.WriteLine("netID: " + dataStruct.netID.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.characterState), 0, data, 12, 4);
            // Console.WriteLine("Charstate: " + dataStruct.characterState.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.plantingTree), 0, data, 20, 4);
            //Console.WriteLine("plantingTree: " + dataStruct.plantingTree.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.x), 0, data, 24, 4);
            //Console.WriteLine("x: " + dataStruct.x.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.y), 0, data, 28, 4);
            // Console.WriteLine("y: " + dataStruct.y.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.XSpeed), 0, data, 32, 4);
            // Console.WriteLine("XSpeed: " + dataStruct.XSpeed.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.YSpeed), 0, data, 36, 4);
            //Console.WriteLine("PACKSPEED: " + BitConverter.ToString(BitConverter.GetBytes(dataStruct.XSpeed)));
            // Console.WriteLine("YSpeed: " + dataStruct.YSpeed.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.punchX), 0, data, 44, 4);
            // Console.WriteLine("punchX: " + dataStruct.punchX.ToString());
            Array.Copy(BitConverter.GetBytes(dataStruct.punchY), 0, data, 48, 4);
            // Console.WriteLine("punchY: " + dataStruct.punchY.ToString());
            return data;
        }

        public static PlayerMoving unpackPlayerMoving(byte[] data)
        {
            PlayerMoving dataStruct = new PlayerMoving();
            dataStruct.packetType = BitConverter.ToInt32(data, 0);
            dataStruct.netID = BitConverter.ToInt32(data, 4);
            dataStruct.characterState = BitConverter.ToInt32(data, 12); ;
            dataStruct.plantingTree = BitConverter.ToInt32(data, 20);
            dataStruct.x = BitConverter.ToInt32(data, 24);


            try
            {
                int i = BitConverter.ToInt32(data, 24);
                byte[] byte2s = BitConverter.GetBytes(i);
                byte b65 = byte2s[1];
                byte b25 = byte2s[2];
                byte b35 = byte2s[3];
                byte[] arr2 = { byte2s[0], b65, b25, b35 };
                float myFloat = System.BitConverter.ToSingle(arr2, 0);
                dataStruct.x = myFloat;


                int i3 = BitConverter.ToInt32(data, 28);
                byte[] byte23s = BitConverter.GetBytes(i3);
                byte b653 = byte23s[1];
                byte b253 = byte23s[2];
                byte b353 = byte23s[3];
                byte[] arr23 = { byte23s[0], b653, b253, b353 };
                float myFloat3 = System.BitConverter.ToSingle(arr23, 0);
                dataStruct.y = myFloat3;
            }
            catch { Exception ex; }


            try
            {
                int i = BitConverter.ToInt32(data, 32);
                byte[] byte2s = BitConverter.GetBytes(i);
                byte b65 = byte2s[1];
                byte b25 = byte2s[2];
                byte b35 = byte2s[3];
                byte[] arr2 = { byte2s[0], b65, b25, b35 };
                float myFloat = System.BitConverter.ToSingle(arr2, 0);
                dataStruct.XSpeed = myFloat;


                int i3 = BitConverter.ToInt32(data, 36);
                byte[] byte23s = BitConverter.GetBytes(i3);
                byte b653 = byte23s[1];
                byte b253 = byte23s[2];
                byte b353 = byte23s[3];
                byte[] arr23 = { byte23s[0], b653, b253, b353 };
                float myFloat3 = System.BitConverter.ToSingle(arr23, 0);
                dataStruct.YSpeed = myFloat3;
            }
            catch { Exception ex; }


            //dataStruct.XSpeed = BitConverter.ToInt32(data, 32);
            //dataStruct.YSpeed = BitConverter.ToInt32(data, 36);
            //Console.WriteLine("UNPACKSPEED: " + BitConverter.ToString(BitConverter.GetBytes(dataStruct.XSpeed)));
            dataStruct.punchX = BitConverter.ToInt32(data, 44);
            dataStruct.punchY = BitConverter.ToInt32(data, 48);
            return dataStruct;
        }

        public void SendPacket(int a1, string a2, ENetPeer enetPeer)
        {
            try
            {
                if (enetPeer != null)
                {
                    byte[] v3 = new byte[a2.Length + 5];
                    Array.Copy(BitConverter.GetBytes(a1), 0, v3, 0, 4);
                    //*(v3->data) = (DWORD)a1;
                    Array.Copy(Encoding.ASCII.GetBytes(a2), 0, v3, 4, a2.Length);

                    //cout << std::hex << (int)(char)v3->data[3] << endl;
                    enetPeer.Send(v3, 0, ENetPacketFlags.Reliable);
                }
            }
            catch
            {
                Console.WriteLine("error in void sendPacket");
            }
        }

        public static void SendPacketRaw(int a1, byte[] packetData, long packetDataSize, int a4, ENetPeer peer, int packetFlag)
        {
            try
            {
                if (peer != null) // check if we have it setup
                {

                    if (a1 == 4 && (packetData[12] & 8) == 1)
                    {
                        byte[] p = new byte[packetDataSize + packetData[13]];
                        Array.Copy(BitConverter.GetBytes(4), 0, p, 0, 4); //right
                        Array.Copy(packetData, 0, p, 4, packetDataSize); //right
                                                                         //Array.Copy(BitConverter.GetBytes(a4), 0, p, 4 + packetDataSize, 4);
                        Array.Copy(BitConverter.GetBytes(a4), 0, p, 4 + packetDataSize, 4 + 13);
                        peer.Send(p, 0, ENetPacketFlags.Reliable);
                        string mo = BitConverter.ToString(p);
                        mo = mo.Replace("-", "");

                    }
                    else
                    {
                        byte[] p = new byte[packetDataSize + 5];
                        Array.Copy(BitConverter.GetBytes(a1), 0, p, 0, 4);
                        //Console.WriteLine("1: " + BitConverter.ToString(p).Replace("-", ""));
                        Array.Copy(packetData, 0, p, 4, packetDataSize);
                        //Console.WriteLine("2: " + BitConverter.ToString(p).Replace("-", ""));
                        peer.Send(p, 0, ENetPacketFlags.Reliable);
                        // Console.WriteLine("Bytelen: " + p.Length + ":Len2: " + packetDataSize.ToString());
                        //  Console.WriteLine("Packetdata: " + BitConverter.ToString(p).Replace("-", ""));
                        string mo = BitConverter.ToString(p);
                        mo = mo.Replace("-", "");

                    }
                }
            }
            catch
            {
                Console.WriteLine("error in sendpacketraw");
            }
        }

        public static void SendPacketRaw2(int a1, byte[] packetData, long packetDataSize, int a4, ENetPeer peer, int packetFlag)
        {
            try
            {
                if (peer != null) // check if we have it setup
                {

                    if (a1 == 4 && (packetData[12] & 8) == 1)
                    {
                        byte[] p = new byte[packetDataSize + packetData[13]];
                        Array.Copy(BitConverter.GetBytes(4), 0, p, 0, 4); //right
                        Array.Copy(packetData, 0, p, 4, packetDataSize); //right
                                                                         //Array.Copy(BitConverter.GetBytes(a4), 0, p, 4 + packetDataSize, 4);
                        Array.Copy(BitConverter.GetBytes(a4), 0, p, 4 + packetDataSize, 4 + 13);
                        peer.Send(p, 0, ENetPacketFlags.Reliable);
                        string mo = BitConverter.ToString(p);
                        mo = mo.Replace("-", "");

                    }
                    else
                    {
                        if (a1 == 192)
                        {
                            a1 = 4;
                            byte[] p = new byte[packetDataSize + 5];
                            Array.Copy(BitConverter.GetBytes(a1), 0, p, 0, 4);
                            Array.Copy(packetData, 0, p, 4, packetDataSize);
                            peer.Send(p, 0, ENetPacketFlags.Reliable);
                            string mo = BitConverter.ToString(p);
                            mo = mo.Replace("-", "");
                        }
                        else
                        {
                            byte[] p = new byte[packetDataSize + 5];
                            Array.Copy(BitConverter.GetBytes(a1), 0, p, 0, 4);
                            Array.Copy(packetData, 0, p, 4, packetDataSize);
                            peer.Send(p, 0, ENetPacketFlags.Reliable);
                            string mo = BitConverter.ToString(p);
                            mo = mo.Replace("-", "");
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in sendpacketraw2");
            }
        }

        public static void showWrong(ENetPeer peer, string listFull, string itemFind)
        {
            try
            {
                GamePacket fff = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wFind item: " + itemFind + "``|left|206|\nadd_spacer|small|\n" + listFull + "add_textbox|Enter a word below to find the item|\nadd_text_input|item|Item Name||30|\nend_dialog|findid|Cancel|Find the item!|\n"));
                peer.Send(fff.data, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in showwrong");
            }
        }

        public static void effect(ENetPeer peer, int punch)
        {
            int netid = (peer.Data as PlayerInfo).netID;
            PlayerInfo info = peer.Data as PlayerInfo;
            int state = getState(info);
            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {
                    PlayerMoving data = new PlayerMoving();
                    data.packetType = 0x14;
                    data.characterState = 0; // animation
                    data.x = 1000;
                    data.y = 100;
                    data.punchX = 0;
                    data.punchY = 0;
                    data.XSpeed = 300;
                    data.YSpeed = 600;
                    data.netID = netid;
                    data.plantingTree = state;
                    byte[] raw = packPlayerMoving(data);
                    int var = punch;
                    Array.Copy(BitConverter.GetBytes(var), 0, raw, 1, 3);
                    SendPacketRaw(4, raw, 56, 0, currentPeer, 0);
                }
            }
        }
        public static void joinworld(ENetPeer peer, string act, int x2, int y2)
        {
            //Console.WriteLine("Entering some world...");
            try
            {
                WorldInfo info = worldDB.get(act);
                string name = act;
                if (name == "CON" || name == "PRN" || name == "AUX" || name == "NUL" || name == "COM1" || name == "COM2" || name == "COM3" || name == "COM4" || name == "COM5" || name == "COM6" || name == "COM7" || name == "COM8" || name == "COM9" || name == "LPT1" || name == "LPT2" || name == "LPT3" || name == "LPT4" || name == "LPT5" || name == "LPT6" || name == "LPT7" || name == "LPT8" || name == "LPT9" || name == "con" || name == "prn" || name == "aux" || name == "nul" || name == "com1" || name == "com2" || name == "com3" || name == "com4" || name == "com5" || name == "com6" || name == "com7" || name == "com8" || name == "com9" || name == "lpt1" || name == "lpt2" || name == "lpt3" || name == "lpt4" || name == "lpt5" || name == "lpt6" || name == "lpt7" || name == "lpt8" || name == "lpt9")
                {
                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                    GamePacket pzo = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "`4Sorry `w this world is used by the system"));
                    peer.Send(pzo.data, 0, ENetPacketFlags.Reliable);
                    GamePacket p3 = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    return;
                }
                if (info.isNuked == true)
                {
                    if ((peer.Data as PlayerInfo).adminLevel >= 666)
                    {
                        GamePacket pzo = packetEnd(appendString(
                         appendString(createPacket(), "OnConsoleMessage"),
                         "`4That world is inaccessible! `$(`2YOU HAVE ACCESS TO ENTER`$)"));
                        peer.Send(pzo.data, 0, ENetPacketFlags.Reliable);
                    }
                    else
                    {
                        GamePacket pz = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "This world is inaccessible!"));
                        peer.Send(pz.data, 0, ENetPacketFlags.Reliable);
                        GamePacket pp = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                        peer.Send(pp.data, 0, ENetPacketFlags.Reliable);
                        return;
                    }
                }
                if (act.Length > 25)
                {
                    GamePacket pzz = packetEnd(appendString(
                     appendString(createPacket(), "OnConsoleMessage"),
                   "`4Sorry, System doesnt accept more than 30 letter in world name, you will be disconnected"));
                    peer.Send(pzz.data, 0, ENetPacketFlags.Reliable);
                    peer.DisconnectLater(0);
                }
                long wow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if ((peer.Data as PlayerInfo).lastjoinreq + 600 < wow)
                {
                    (peer.Data as PlayerInfo).lastjoinreq = wow;
                }
                else
                {
                    GamePacket p1 = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "`4Please wait you will be disconnected..!"));
                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                    peer.DisconnectLater(0);
                }
                if ((peer.Data as PlayerInfo).canleave == 1)
                {
                    WorldInfo info1 = worldDB.get("HELL");
                    sendWorld(peer, info1);
                }
                else
                {
                    sendWorld(peer, info);
                }
                int x = 3040;
                int y = 736;

                for (int j = 0; j < info.width * info.height; j++)
                {
                    if (info.items[j].foreground == 6)
                    {
                        x = (j % info.width) * 32;
                        y = (j / info.width) * 32;
                    }
                }
                (peer.Data as PlayerInfo).cpX = x;
                (peer.Data as PlayerInfo).cpY = y;
                if (x2 != 0 && y2 != 0)
                {
                    x = x2;
                    y = y2;
                }
                int id = 244;

                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnSpawn"), "spawn|avatar\nnetID|" + cId + "\nuserID|" + cId + "\ncolrect|0|0|20|30\nposXY|" + x + "|" + y + "\nname|``" + (peer.Data as PlayerInfo).displayName + "``\ncountry|" + (peer.Data as PlayerInfo).country + "|" + id + "\ninvis|0\nmstate|0\nsmstate|1\ntype|local\n"));
                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                //enet_host_flush(server);
                (peer.Data as PlayerInfo).netID = cId;
                onPeerConnect(peer);
                cId++;

                sendInventory(peer, (peer.Data as PlayerInfo).inventory);

            }
            catch
            {
                int e = 0;
                if (e == 1)
                {
                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                    GamePacket p = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "You have exited the world."));

                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    GamePacket p3 = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    return;
                    //enet_host_flush(server);
                }
                else if (e == 2)
                {
                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                    GamePacket p = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "You have entered bad characters in the world name!"));

                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    GamePacket p3 = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    return;
                    //enet_host_flush(server);
                }
                else if (e == 3)
                {
                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                    GamePacket p = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "Exit from what? Click back if you're done playing."));

                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    GamePacket p3 = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    return;
                    //enet_host_flush(server);
                }
                else
                {
                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                    GamePacket p = packetEnd(appendString(
                        appendString(createPacket(), "OnConsoleMessage"),
                        "I know this menu is magical and all, but it has its limitations! You can't visit this world!"));

                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    GamePacket p3 = packetEnd(appendString(appendInt(appendString(createPacket(), "OnFailedToEnterWorld"), 1), "Sorry"));
                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    return;
                    //enet_host_flush(server);
                }
            }
        }
        public static void onPeerConnect(ENetPeer peer)
        {
            try
            {
                for (int i = 0; i < peers.Count; i++)
                {
                    ENetPeer currentPeer = peers[i];
                    if (peers[i].State != ENetPeerState.Connected)
                        continue;
                    if (peer != currentPeer)
                    {
                        if (isHere(peer, currentPeer))
                        {
                            string netIdS = (currentPeer.Data as PlayerInfo).netID.ToString();
                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnSpawn"), "spawn|avatar\nnetID|" + netIdS + "\nuserID|" + (currentPeer.Data as PlayerInfo).netID.ToString() + "\ncolrect|0|0|20|30\nposXY|" + ((currentPeer.Data as PlayerInfo).x).ToString() + "|" + ((currentPeer.Data as PlayerInfo).y).ToString() + "\nname|``" + (currentPeer.Data as PlayerInfo).displayName + "``\ncountry|" + (currentPeer.Data as PlayerInfo).country + "\ninvis|0\nmstate|0\nsmstate|0\n")); // ((PlayerInfo*)(server->peers[i].data))->tankIDName
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        //GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnSpawn"), "spawn|avatar\nnetID|" + netIdS + "\nuserID|" + netIdS + "\ncolrect|0|0|20|30\nposXY|1600|1154\nname|``" + (currentPeer.Data as PlayerInfo).displayName + "``\ncountry|" + (currentPeer.Data as PlayerInfo).country + "\ninvis|0\nmstate|0\nsmstate|0\n")); // ((PlayerInfo*)(server->peers[i].data))->tankIDName
                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);



                            string netIdS2 = (peer.Data as PlayerInfo).netID.ToString();
                            GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnSpawn"), "spawn|avatar\nnetID|" + netIdS2 + "\nuserID|" + (peer.Data as PlayerInfo).netID.ToString() + "\ncolrect|0|0|20|30\nposXY|" + ((peer.Data as PlayerInfo).x).ToString() + "|" + ((peer.Data as PlayerInfo).y).ToString() + "\nname|``" + (peer.Data as PlayerInfo).displayName + "``\ncountry|" + (peer.Data as PlayerInfo).country + "\ninvis|0\nmstate|0\nsmstate|0\n")); // ((PlayerInfo*)(server->peers[i].data))->tankIDName
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       //GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnSpawn"), "spawn|avatar\nnetID|" + netIdS2 + "\nuserID|" + netIdS2 + "\ncolrect|0|0|20|30\nposXY|1600|1154\nname|``" + (peer.Data as PlayerInfo).displayName + "``\ncountry|" + (peer.Data as PlayerInfo).country + "\ninvis|0\nmstate|0\nsmstate|0\n")); // ((PlayerInfo*)(server->peers[i].data))->tankIDName
                            currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                            //enet_host_flush(server);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in onpeerconnect");
            }
        }

        public static void updateAllClothes(ENetPeer peer)
        {
            try
            {
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        GamePacket p3 = packetEnd(appendFloat(appendIntx(appendFloat(appendFloat(appendFloat(appendString(createPacket(), "OnSetClothing"), (peer.Data as PlayerInfo).cloth_hair, (peer.Data as PlayerInfo).cloth_shirt, (peer.Data as PlayerInfo).cloth_pants), (peer.Data as PlayerInfo).cloth_feet, (peer.Data as PlayerInfo).cloth_face, (peer.Data as PlayerInfo).cloth_hand), (peer.Data as PlayerInfo).cloth_back, (peer.Data as PlayerInfo).cloth_mask, (peer.Data as PlayerInfo).cloth_necklace), (int)(peer.Data as PlayerInfo).skinColor), (peer.Data as PlayerInfo).cloth_ances, 0.0f, 0.0f));
                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4); // ffloor

                        currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                        //enet_host_flush(server);
                        GamePacket p4 = packetEnd(appendFloat(appendIntx(appendFloat(appendFloat(appendFloat(appendString(createPacket(), "OnSetClothing"), (currentPeer.Data as PlayerInfo).cloth_hair, (currentPeer.Data as PlayerInfo).cloth_shirt, (currentPeer.Data as PlayerInfo).cloth_pants), (currentPeer.Data as PlayerInfo).cloth_feet, (currentPeer.Data as PlayerInfo).cloth_face, (currentPeer.Data as PlayerInfo).cloth_hand), (currentPeer.Data as PlayerInfo).cloth_back, (currentPeer.Data as PlayerInfo).cloth_mask, (currentPeer.Data as PlayerInfo).cloth_necklace), (int)(currentPeer.Data as PlayerInfo).skinColor), (currentPeer.Data as PlayerInfo).cloth_ances, 0.0f, 0.0f));
                        Array.Copy(BitConverter.GetBytes((currentPeer.Data as PlayerInfo).netID), 0, p4.data, 8, 4); // ffloor
                        peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                    }
                }
            }
            catch
            {
                Console.WriteLine("void updateallclothes");
            }
        }

        public static void sendClothes(ENetPeer peer)
        {
            try
            {
                GamePacket p3 = packetEnd(appendFloat(appendIntx(appendFloat(appendFloat(appendFloat(appendString(createPacket(), "OnSetClothing"), (peer.Data as PlayerInfo).cloth_hair, (peer.Data as PlayerInfo).cloth_shirt, (peer.Data as PlayerInfo).cloth_pants), (peer.Data as PlayerInfo).cloth_feet, (peer.Data as PlayerInfo).cloth_face, (peer.Data as PlayerInfo).cloth_hand), (peer.Data as PlayerInfo).cloth_back, (peer.Data as PlayerInfo).cloth_mask, (peer.Data as PlayerInfo).cloth_necklace), (int)(peer.Data as PlayerInfo).skinColor), (peer.Data as PlayerInfo).cloth_ances, 0.0f, 0.0f));
                for (int i = 0; i < peers.Count; i++)
                {
                    ENetPeer currentPeer = peers[i];
                    if (peers[i].State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4); // ffloor
                        currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                    }

                }
            }
            catch
            {
                Console.WriteLine("error in void sendclothes");
            }
        }

        public static void sendPData(ENetPeer peer, PlayerMoving data)
        {
            try
            {
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (peer != currentPeer)
                    {
                        if (isHere(peer, currentPeer))
                        {
                            data.netID = (peer.Data as PlayerInfo).netID;
                            SendPacketRaw(4, packPlayerMoving(data), 56, 0, currentPeer, 0);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in void sendPData");
            }
        }

        public static int getPlayersCountInWorld(string name)
        {
            int count = 0;
            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if ((currentPeer.Data as PlayerInfo).currentWorld == name)
                    count++;
            }
            return count;
        }

        public static void sendRoulete(ENetPeer peer, int x, int y)
        {
            try
            {
                Random rand = new Random();
                int val = rand.Next(0, 37);
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        GamePacket p2 = packetEnd(appendIntx(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`w[" + (peer.Data as PlayerInfo).displayName + " `wspun the wheel and got `6" + val + "`w!]"), 0));
                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                    }

                    //cout << "Tile update at: " << data2->punchX << "x" << data2->punchY << endl;
                }
            }
            catch
            {
                Console.WriteLine("error in void sendRoulete");
            }
        }

        public static void sendNothingHappened(ENetPeer peer, int x, int y)
        {
            PlayerMoving data = new PlayerMoving();
            data.netID = (peer.Data as PlayerInfo).netID;
            data.packetType = 0x8;
            data.plantingTree = 0;
            data.netID = -1;
            data.x = x;
            data.y = y;
            data.punchX = x;
            data.punchY = y;
            SendPacketRaw(4, packPlayerMoving(data), 56, 0, peer, 0);
        }

        public static void InitializePacketWithDisplayBlock(byte[] raw)
        {
            try
            {
                int i = 0;
                raw[i] = 0x05; i++; // 0
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 4
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 8
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x08; i++; // 12
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 16
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 20
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 24
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 28
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 32
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 36
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 40
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0xff; i++; // 44
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0xff; i++; // 48
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++; // 52
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x82; i++;
                raw[i] = 0x0b; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x01; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x17; i++;
                raw[i] = 0x82; i++;
                raw[i] = 0x04; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x00; i++;
                raw[i] = 0x6c; i++;
                raw[i] = 0xfd; i++;
                raw[i] = 0xfd; i++;
                raw[i] = 0xfd; i++;
            }
            catch
            {
                Console.WriteLine("error in intilizewithdisplayblock");
            }
        }
        public static void sendtradeaff(ENetPeer peer, int id, int netIDsrc, int netIDdst, int timeMs)
        {
            try
            {
                PlayerMoving data = new PlayerMoving();
                data.packetType = 0x13; data.punchX = id; data.punchY = id;
                byte[] raw = packPlayerMoving(data);
                int netIdSrc = netIDsrc; int netIdDst = netIDdst; int three = 3; int n1 = timeMs;
                Array.Copy(BitConverter.GetBytes(three), 0, raw, 3, 1);
                Array.Copy(BitConverter.GetBytes(netIdDst), 0, raw, 4, 4);
                Array.Copy(BitConverter.GetBytes(netIdSrc), 0, raw, 8, 4);
                Array.Copy(BitConverter.GetBytes(n1), 0, raw, 20, 4);
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        byte[] raw2 = new byte[56];
                        Array.Copy(raw, 0, raw2, 0, 56);
                        SendPacketRaw(4, raw2, 56, 0, peer, 0);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in sendtradeaff");
            }
        }

        public static void updatevend(ENetPeer peer, int x, int y, int id, bool locks, int price)
        {
            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {
                    WorldInfo world = getPlyersWorld(peer);
                    TileExtra data = new TileExtra();
                    data.packetType = 0x5;
                    data.characterState = 8;
                    data.punchX = x;
                    data.punchY = y;
                    data.charStat = 13;
                    data.blockid = 2978;
                    data.backgroundid = world.items[x + (y * world.width)].background;
                    data.visual = 0x00410000;
                    if (locks == true) data.visual = 0x02410000;
                    int n = id;// the id
                    string hex = "";
                    {
                        string ssl = DecimalToHexadecimal(n);
                        string res = ssl;
                        hex = res + "18";
                    }
                    string ss = hex;
                    int xs = Convert.ToInt32(ss, 16);
                    data.displayblock = xs;

                    int xes;
                    {
                        string hexg = "";
                        {
                            int wl = price; // 2 world lock each
                            string ss2 = DecimalToHexadecimal(wl);
                            string res = ss2;
                            hexg = res + "00";
                        }
                        string ssz = hexg;
                        int xx = Convert.ToInt32(ssz, 16);
                        xes = xx;
                    }
                    byte[] raw = null;
                    raw = packvisual(data, 0, xes);
                    SendPacketRaw2(192, raw, 102, 0, currentPeer, 0);
                    raw = null;
                }
            }
        }
        public static void updatestuffweather(ENetPeer peer, int x, int y, int tile, int bg, int gravity, bool isInverted, bool isSpinning)
        {
            try
            {
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {

                        TileExtra data = new TileExtra();
                        data.packetType = 0x5;
                        data.characterState = 8;
                        data.punchX = x;
                        data.punchY = y;
                        data.charStat = 18; // 13
                        data.blockid = 3832;
                        data.backgroundid = bg; // 2946
                        data.visual = 0;
                        int n = tile;
                        string hex = "";
                        {
                            string ssl = DecimalToHexadecimal(n);
                            string res = ssl;
                            hex = res + "31";
                        }
                        int gravi = gravity;
                        string hexg = "";
                        {
                            int temp = gravi;
                            if (gravi < 0) temp = -gravi;
                            string ss12 = DecimalToHexadecimal(temp);
                            string res12 = ss12;
                            hexg = res12 + "00";
                        }
                        string ss = hex;
                        int xx = Convert.ToInt32(ss, 16);
                        data.displayblock = xx;
                        string sss = hexg;
                        int xxs = Convert.ToInt32(sss, 16);
                        if (gravi < 0) xxs = -xxs;
                        if (gravi < 0)
                        {
                            SendPacketRaw2(192, packvisual(data, 0x03FFFFFF, xxs), 102, 0, currentPeer, 0);
                        }
                        else
                        {
                            SendPacketRaw2(192, packvisual(data, 0x02000000, xxs), 102, 0, currentPeer, 0);
                        }

                        GamePacket p = packetEnd(appendInt(
                        appendString(createPacket(), "OnSetCurrentWeather"), 29));
                        currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in void stuffweather");
            }
        }

        public static void tradestatus(ENetPeer peer, int netid, string s2, string offername, string box)
        {
            try
            {
                GamePacket p2t = packetEnd(appendString(appendString(appendString(appendInt(appendString(createPacket(), "OnTradeStatus"), netid), s2), offername + "`o's offer."), box));
                peer.Send(p2t.data, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in tradestatus");
            }
        }
        public static void updatesign(ENetPeer peer, int foreground, int background, int x, int y, string text)
        {
            try
            {
                int hmm = 8; int text_len = text.Length; int lol = 0; int wut = 5; int yeh = hmm + 3 + 1; int idk = 15 + text_len;
                int is_locked = 0; int bubble_type = 2; int ok = 52 + idk; int kek = ok + 4; int yup = ok - 8 - idk; int four = 4;
                int magic = 56; int wew = ok + 5 + 4; int wow = magic + 4 + 5;
                byte[] data = new byte[kek]; byte[] p = new byte[wew];
                for (int i = 0; i < kek; i++) data[i] = 0;
                Array.Copy(BitConverter.GetBytes(wut), 0, data, 0, four);
                Array.Copy(BitConverter.GetBytes(hmm), 0, data, yeh, four);
                Array.Copy(BitConverter.GetBytes(x), 0, data, yup, 4);
                Array.Copy(BitConverter.GetBytes(y), 0, data, yup + 4, 4);
                Array.Copy(BitConverter.GetBytes(idk), 0, data, 4 + yup + 4, four);
                Array.Copy(BitConverter.GetBytes(foreground), 0, data, magic, 2);
                Array.Copy(BitConverter.GetBytes(background), 0, data, magic + 2, 2);
                Array.Copy(BitConverter.GetBytes(lol), 0, data, four + magic, four);
                Array.Copy(BitConverter.GetBytes(bubble_type), 0, data, magic + 4 + four, 1);
                Array.Copy(BitConverter.GetBytes(text_len), 0, data, wow, 2);
                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 2 + wow, text_len);
                Array.Copy(BitConverter.GetBytes(is_locked), 0, data, ok, four);
                Array.Copy(BitConverter.GetBytes(four), 0, p, 0, four);
                Array.Copy(data, 0, p, four, kek);
                peer.Send(p, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in updatesign");
            }
        }
        public static void respawn(ENetPeer peer, bool die)
        {
            try
            {
                if (die == false)
                {
                    GamePacket p1 = packetEnd(appendString(createPacket(), "OnKilled"));
                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p1.data, 8, 4);
                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                }
                GamePacket p2x = packetEnd(appendInt(appendString(createPacket(), "OnSetFreezeState"), 0));
                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2x.data, 8, 4);
                int respawnTimeout = 2000;
                int deathFlag = 0x19;
                Array.Copy(BitConverter.GetBytes(respawnTimeout), 0, p2x.data, 24, 4);
                Array.Copy(BitConverter.GetBytes(deathFlag), 0, p2x.data, 56, 4);
                peer.Send(p2x.data, 0, ENetPacketFlags.Reliable);
                GamePacket pf = packetEnd(appendInt(appendString(createPacket(), "OnSetFreezeState"), 2));
                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, pf.data, 8, 4);
                peer.Send(pf.data, 0, ENetPacketFlags.Reliable);
                GamePacket p2 = packetEnd(appendFloat(appendString(createPacket(), "OnSetPos"), (peer.Data as PlayerInfo).cpX, (peer.Data as PlayerInfo).cpY));
                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                respawnTimeout = 2000;
                Array.Copy(BitConverter.GetBytes(respawnTimeout), 0, p2.data, 24, 4);
                Array.Copy(BitConverter.GetBytes(deathFlag), 0, p2.data, 56, 4);
                peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                GamePacket p2a = packetEnd(appendString(appendString(createPacket(), "OnPlayPositioned"), "audio/teleport.wav"));
                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2a.data, 8, 4);
                respawnTimeout = 2000;
                Array.Copy(BitConverter.GetBytes(respawnTimeout), 0, p2a.data, 24, 4);
                Array.Copy(BitConverter.GetBytes(deathFlag), 0, p2a.data, 56, 4);
                peer.Send(p2a.data, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in void respawn");
            }
        }

        public static void updateEntrance(ENetPeer peer, int foreground, int x, int y, bool open, int bg)
        {
            byte[] data = new byte[69];
            for (int i = 0; i < 69; i++) data[i] = 0;
            int four = 4; int five = 5; int eight = 8;
            int huhed = (65536 * bg) + foreground; int loled = 128;
            Array.Copy(BitConverter.GetBytes(four), 0, data, 0, 4);
            Array.Copy(BitConverter.GetBytes(five), 0, data, 4, 4);
            Array.Copy(BitConverter.GetBytes(eight), 0, data, 16, 4);
            Array.Copy(BitConverter.GetBytes(x), 0, data, 48, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, data, 52, 4);
            Array.Copy(BitConverter.GetBytes(eight), 0, data, 56, 4);
            Array.Copy(BitConverter.GetBytes(foreground), 0, data, 60, 4);
            Array.Copy(BitConverter.GetBytes(bg), 0, data, 62, 4);
            if (open)
            {
                int state = 0;
                Array.Copy(BitConverter.GetBytes(loled), 0, data, 66, 4);
                Array.Copy(BitConverter.GetBytes(state), 0, data, 68, 4);
            }
            else
            {
                int state = 100;
                int yeetus = 25600;
                Array.Copy(BitConverter.GetBytes(yeetus), 0, data, 67, 5);
                Array.Copy(BitConverter.GetBytes(state), 0, data, 68, 4);
            }
            byte[] p = new byte[69];
            peer.Send(p, 0, ENetPacketFlags.Reliable);
        }
        public static void updatedisplayblock(ENetPeer peer, int foreground, int x, int y, int background, int itemid)
        {
            try
            {
                int plength = 74;
                byte[] raw = new byte[plength];
                for (int i = 0; i < 74; i++) raw[i] = 0;
                InitializePacketWithDisplayBlock(raw);
                Array.Copy(BitConverter.GetBytes(x), 0, raw, 44, sizeof(int));
                Array.Copy(BitConverter.GetBytes(y), 0, raw, 48, sizeof(int));
                Array.Copy(BitConverter.GetBytes(foreground), 0, raw, 56, sizeof(short));
                Array.Copy(BitConverter.GetBytes(background), 0, raw, 58, sizeof(short));
                Array.Copy(BitConverter.GetBytes(itemid), 0, raw, 65, sizeof(int));
                byte[] p = new byte[plength + 4];
                int four = 4;
                Array.Copy(BitConverter.GetBytes(four), 0, p, 0, sizeof(int));
                Array.Copy(raw, 0, p, 4, plength);
                peer.Send(p, 0, ENetPacketFlags.Reliable);
            }
            catch
            {
                Console.WriteLine("error in void updatedisplayblock");
            }
        }

        public static void updatevendtext(ENetPeer peer, int foreground, int x, int y, string text)
        {
            try
            {
                PlayerMoving sign = new PlayerMoving();
                sign.packetType = 0x3; sign.characterState = 0x0; sign.x = x; sign.y = y; sign.punchX = x; sign.punchY = y;
                sign.netID = -1; sign.plantingTree = foreground;
                SendPacketRaw(4, packPlayerMoving(sign), 56, 0, peer, 0);
                int hmm = 8; int text_len = text.Length; int lol = 0; int wut = 5; int yeh = hmm + 3 + 1; int idk = 15 + text_len;
                int is_locked = 0; int bubble_type = 21; int ok = 52 + idk; int kek = ok + 4; int yup = ok - 8 - idk; int magic = 56;
                int wew = ok + 5 + 4; int wow = magic + 4 + 5; int four = 4;
                byte[] data = new byte[kek]; byte[] p = new byte[wew];
                for (int i = 0; i < kek; i++) data[i] = 0;
                Array.Copy(BitConverter.GetBytes(wut), 0, data, 0, four);
                Array.Copy(BitConverter.GetBytes(hmm), 0, data, yeh, four);
                Array.Copy(BitConverter.GetBytes(x), 0, data, yup, 4);
                Array.Copy(BitConverter.GetBytes(y), 0, data, yup + 4, 4);
                Array.Copy(BitConverter.GetBytes(idk), 0, data, 4 + yup + 4, four);
                Array.Copy(BitConverter.GetBytes(foreground), 0, data, magic, 2);
                Array.Copy(BitConverter.GetBytes(lol), 0, data, four + magic, four);
                Array.Copy(BitConverter.GetBytes(bubble_type), 0, data, magic + 4 + four, 1);
                Array.Copy(BitConverter.GetBytes(text_len), 0, data, wow, 2);
                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 2 + wow, text_len);
                Array.Copy(BitConverter.GetBytes(is_locked), 0, data, ok, four);
                Array.Copy(BitConverter.GetBytes(four), 0, p, 0, four);
                Array.Copy(data, 0, p, four, kek);
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        currentPeer.Send(p, 0, ENetPacketFlags.Reliable);
                    }
                }
            }
            catch
            {

            }
        }


        public static void updatedoor(ENetPeer peer, int foreground, int x, int y, string text)
        {
            try
            {
                PlayerMoving sign = new PlayerMoving();
                sign.packetType = 0x3; sign.characterState = 0x0; sign.x = x; sign.y = y; sign.punchX = x; sign.punchY = y;
                sign.netID = -1; sign.plantingTree = foreground;
                SendPacketRaw(4, packPlayerMoving(sign), 56, 0, peer, 0);
                int hmm = 8; int text_len = text.Length; int lol = 0; int wut = 5; int yeh = hmm + 3 + 1; int idk = 15 + text_len;
                int is_locked = 0; int bubble_type = 1; int ok = 52 + idk; int kek = ok + 4; int yup = ok - 8 - idk; int magic = 56;
                int wew = ok + 5 + 4; int wow = magic + 4 + 5; int four = 4;
                byte[] data = new byte[kek]; byte[] p = new byte[wew];
                for (int i = 0; i < kek; i++) data[i] = 0;
                Array.Copy(BitConverter.GetBytes(wut), 0, data, 0, four);
                Array.Copy(BitConverter.GetBytes(hmm), 0, data, yeh, four);
                Array.Copy(BitConverter.GetBytes(x), 0, data, yup, 4);
                Array.Copy(BitConverter.GetBytes(y), 0, data, yup + 4, 4);
                Array.Copy(BitConverter.GetBytes(idk), 0, data, 4 + yup + 4, four);
                Array.Copy(BitConverter.GetBytes(foreground), 0, data, magic, 2);
                Array.Copy(BitConverter.GetBytes(lol), 0, data, four + magic, four);
                Array.Copy(BitConverter.GetBytes(bubble_type), 0, data, magic + 4 + four, 1);
                Array.Copy(BitConverter.GetBytes(text_len), 0, data, wow, 2);
                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 2 + wow, text_len);
                Array.Copy(BitConverter.GetBytes(is_locked), 0, data, ok, four);
                Array.Copy(BitConverter.GetBytes(four), 0, p, 0, four);
                Array.Copy(data, 0, p, four, kek);
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        currentPeer.Send(p, 0, ENetPacketFlags.Reliable);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in updatedoor");
            }
        }

        public static void lockdoor(ENetPeer peer, int foreground, int x, int y, string text)
        {
            try
            {
                PlayerMoving sign = new PlayerMoving();
                sign.packetType = 0x3; sign.characterState = 0x0; sign.x = x; sign.y = y; sign.punchX = x; sign.punchY = y;
                sign.netID = -1; sign.plantingTree = foreground;
                SendPacketRaw(4, packPlayerMoving(sign), 56, 0, peer, 0);
                int hmm = 8; int text_len = text.Length; int lol = 0; int wut = 5; int yeh = hmm + 3 + 1; int idk = 15 + text_len;
                int is_locked = -1; int bubble_type = 1; int ok = 52 + idk; int kek = ok + 4; int yup = ok - 8 - idk; int magic = 56;
                int wew = ok + 5 + 4; int wow = magic + 4 + 5; int four = 4;
                byte[] data = new byte[kek]; byte[] p = new byte[wew];
                for (int i = 0; i < kek; i++) data[i] = 0;
                Array.Copy(BitConverter.GetBytes(wut), 0, data, 0, four);
                Array.Copy(BitConverter.GetBytes(hmm), 0, data, yeh, four);
                Array.Copy(BitConverter.GetBytes(x), 0, data, yup, 4);
                Array.Copy(BitConverter.GetBytes(y), 0, data, yup + 4, 4);
                Array.Copy(BitConverter.GetBytes(idk), 0, data, 4 + yup + 4, four);
                Array.Copy(BitConverter.GetBytes(foreground), 0, data, magic, 2);
                Array.Copy(BitConverter.GetBytes(lol), 0, data, four + magic, four);
                Array.Copy(BitConverter.GetBytes(bubble_type), 0, data, magic + 4 + four, 1);
                Array.Copy(BitConverter.GetBytes(text_len), 0, data, wow, 2);
                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 2 + wow, text_len);
                Array.Copy(BitConverter.GetBytes(is_locked), 0, data, ok, four);
                Array.Copy(BitConverter.GetBytes(four), 0, p, 0, four);
                Array.Copy(data, 0, p, four, kek);
                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        currentPeer.Send(p, 0, ENetPacketFlags.Reliable);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in lockdoor");
            }
        }
        public static void sendTileUpdate(int x, int y, int tile, int causedBy, ENetPeer peer)
        {
            try
            {
                PlayerMoving data = new PlayerMoving();
                data.packetType = 0x3;

                data.characterState = 0x0; // animation
                data.x = x;
                data.y = y;
                data.punchX = x;
                data.punchY = y;
                data.XSpeed = 0;
                data.YSpeed = 0;
                data.netID = causedBy;
                data.plantingTree = tile;

                WorldInfo world = getPlyersWorld(peer);

                if (world == null) return;
                if (x < 0 || y < 0 || x > world.width || y > world.height) return;
                sendNothingHappened(peer, x, y);
                if ((peer.Data as PlayerInfo).adminLevel < 666)
                {
                    if (world.items[x + (y * world.width)].foreground == 6 || world.items[x + (y * world.width)].foreground == 8 || world.items[x + (y * world.width)].foreground == 3760)
                        return;
                    if (tile == 6 || tile == 8 || tile == 3760)
                        return;
                }
                if ((peer.Data as PlayerInfo).rawName != world.owner)
                {
                    if (world.items[x + (y * world.width)].foreground == 242) return;
                }
                if (world.name == "ADMIN" && getAdminLevel((peer.Data as PlayerInfo).rawName, (peer.Data as PlayerInfo).tankIDPass) == 0)
                {
                    if (world.items[x + (y * world.width)].foreground == 758)
                        sendRoulete(peer, x, y);
                    return;
                }
                if (world.items[x + (y * world.width)].foreground == 2946 && tile != 18 && tile != 32)
                {
                    string haveaccess1 = world.access;
                    string access1 = "";
                    foreach (string line in haveaccess1.Split(",".ToCharArray()))
                    {
                        string[] ex = explode("|", line);
                        string idk = ex[0];

                        access1 += idk;
                    }
                    if ((peer.Data as PlayerInfo).rawName == world.owner || (peer.Data as PlayerInfo).rawName == access1 || (peer.Data as PlayerInfo).adminLevel >= 666)
                    {
                        world.items[x + (y * world.width)].dblock = tile;
                        foreach (ENetPeer currentPeer in peers)
                        {
                            if (currentPeer.State != ENetPeerState.Connected)
                                continue;
                            if (isHere(peer, currentPeer))
                            {
                                updatedisplayblock(currentPeer, 2946, x, y, world.items[x + (y * world.width)].background, tile);
                            }
                        }
                    }
                }
                if (world.name != "ADMIN")
                {
                    if (world.owner != "")
                    {
                        string haveaccess1 = world.access;
                        string access1 = "";
                        int block = world.items[x + (y * world.width)].foreground;
                        foreach (string line in haveaccess1.Split(",".ToCharArray()))
                        {
                            string[] ex = explode("|", line);
                            string idk = ex[0];

                            access1 += idk;
                        }
                        if (block == 2978 && tile == 32) //vending machine
                        {
                            if (x != 0)
                            {
                                (peer.Data as PlayerInfo).lastPunchX = x;
                            }
                            if (y != 0)
                            {
                                (peer.Data as PlayerInfo).lastPunchY = y;
                            }
                            //if owner or access or mod
                            if ((peer.Data as PlayerInfo).rawName == world.owner || (peer.Data as PlayerInfo).rawName == access1 || (peer.Data as PlayerInfo).adminLevel >= 666)
                            {
                                if (world.items[x + (y * world.width)].invend != 0 && world.items[x + (y * world.width)].sold == false)
                                {
                                    int oo = world.items[x + (y * world.width)].invend;
                                    string itemname = itemDefs[oo].name;
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wVending Machine``|left|2978|\nadd_spacer|small|\nadd_label_with_icon|small|`oThe machine contains 1 `2" + itemname + "|left|" + oo + "|\nadd_spacer|small|\nadd_label|small|Not currently for sale|\nadd_button|emptyvend|Empty the machine|\nadd_smalltext|`5(Vending Machine will not function when price is set to 0)|\nadd_text_input|sign|Price ||6||\nadd_quick_exit|\nend_dialog|vendok|Close|Update"));
                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                                else if (world.items[x + (y * world.width)].sold == true)
                                {

                                }
                                else
                                {
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wEdit " + itemDefs[block].name + "``|left|" + block + "|\nadd_spacer|small|\nadd_label|small|`oThis machine is empty.|left|4|\nadd_item_picker|iteminvend|`wPut an item in|Choose an item you want to put in the machine!|nadd_quick_exit|\nend_dialog|lol|Close|"));
                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                            }
                            // not owner or access or mod
                            else
                            {

                            }
                        }
                        if ((peer.Data as PlayerInfo).rawName == world.owner || (peer.Data as PlayerInfo).rawName == access1 || (peer.Data as PlayerInfo).adminLevel >= 666)
                        {
                            // WE ARE GOOD TO GO
                            if (tile == 32)
                            {
                                if (block == 3832)
                                { // stuff weather dialog
                                    if (x != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchX = x;
                                    }
                                    if (y != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchY = y;
                                    }
                                    GamePacket p4 = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wStuff Weather Machine``|left|3832|\nadd_item_picker|stuffitem|Edit Item|Choose any item you want to pick|\nadd_spacer|small|\nadd_text_input|gravity|Gravity Value||4|\nadd_spacer|small|\nadd_quick_exit|\nend_dialog|stuff|Nevermind||"));
                                    peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                }
                                if (block == 242)
                                {
                                    if ((peer.Data as PlayerInfo).rawName != world.owner) return;
                                    int ischeck = 0;
                                    string access = "";
                                    if (world.isPublic == true)
                                    {
                                        ischeck = 1;
                                    }
                                    if (world.access == "")
                                    {
                                        GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\nadd_label_with_icon|big|`wEdit World Lock``|left|242|\nadd_label|small|`wAccess list:``|left\nadd_spacer|small|\nadd_label|small|Currently, you're the only one with access.``|left\nadd_spacer|small|\nadd_player_picker|netid|`wAdd``|\nadd_checkbox|isWorldPublic|Allow anyone to build|" + ischeck + "\nend_dialog|wl_edit|Cancel|OK|"));
                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                    }
                                    else
                                    {
                                        string haveaccess = world.access;
                                        foreach (string line in haveaccess.Split(",".ToCharArray()))
                                        {
                                            string[] ex = explode("|", line);
                                            string idk = ex[0];

                                            access += "\nadd_checkbox|isAccessed|" + idk + "|0|";
                                        }
                                        GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\nadd_label_with_icon|big|`wEdit World Lock``|left|242|\nadd_label|small|`wAccess list:``|left\nadd_spacer|small|" + access + "\nadd_spacer|small|\nadd_player_picker|netid|`wAdd``|\nadd_checkbox|isWorldPublic|Allow anyone to build|" + ischeck + "\nend_dialog|wl_edit|Cancel|OK|"));
                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                    }
                                }
                                if (block == 20) // add sign ids
                                {
                                    if (x != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchX = x;
                                    }
                                    if (y != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchY = y;
                                    }
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wEdit Sign``|left|20|\n\nadd_textbox|`oWhat would you like to write on this sign?|\nadd_text_input|sign|||100|\nend_dialog|signok|Cancel|OK|\n"));
                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                                //enet_host_flush(server);
                                if (block == 12)
                                {
                                    if (x != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchX = x;
                                    }
                                    if (y != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchY = y;
                                    }
                                    int ischeck = 0;
                                    if (world.items[x + (y * world.width)].iop == 1)
                                    {
                                        ischeck = 1;
                                    }
                                    string dest = world.items[x + (y * world.width)].dest;
                                    string label = world.items[x + (y * world.width)].dtext;
                                    string id = world.items[x + (y * world.width)].did;
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wEdit Door``|left|" + block + "||\n\nadd_text_input|dest|`oTarget World|" + dest + "|100|\nadd_text_input|label|Display Label (optional)|" + label + "|100|\nadd_text_input|doorid|ID (optional)|" + id + "|35|\nadd_checkbox|isopenpublic|is open to public|" + ischeck + "\nend_dialog|editdoor|Cancel|OK|"));
                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                                if (block == 224)
                                {
                                    if (x != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchX = x;
                                    }
                                    if (y != 0)
                                    {
                                        (peer.Data as PlayerInfo).lastPunchY = y;
                                    }
                                    int checkss = 0;
                                    if (world.items[x + (y * world.width)].iop == 1)
                                    {
                                        checkss = 1;
                                    }
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wEdit " + itemDefs[block].name + "``|left|" + block + "|\nadd_checkbox|ishpub|`2Public?|" + checkss + "|\nend_dialog|editentranceo|Cancel|`wOK|\nadd_quick_exit|\n"));
                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                            }
                        }
                        else if (world.isPublic)
                        {
                            if (world.items[x + (y * world.width)].foreground == 242)
                            {
                                return;
                            }
                        }
                        else
                        {

                            return;
                        }
                        if (tile == 242)
                        {
                            return;
                        }
                    }
                }

                if (tile == 32)
                {
                    // TODO
                    return;
                }
                if (tile == 822)
                {
                    world.items[x + (y * world.width)].water = !world.items[x + (y * world.width)].water;
                    return;
                }
                if (tile == 3062)
                {
                    world.items[x + (y * world.width)].fire = !world.items[x + (y * world.width)].fire;
                    return;
                }
                if (tile == 1866)
                {
                    world.items[x + (y * world.width)].glue = !world.items[x + (y * world.width)].glue;
                    return;
                }
                ItemDefinition def;
                try
                {
                    def = getItemDef(tile);
                    if (def.clothType != ClothTypes.NONE) return;
                }
                catch
                {
                    def.breakHits = 4;
                    def.blockType = BlockTypes.UNKNOWN;
                }

                if (tile == 544 || tile == 546 || tile == 4520 || tile == 382 || tile == 3116 || tile == 4520 || tile == 1792 || tile == 5666 || tile == 2994 || tile == 4368) return;
                if (tile == 5708 || tile == 5709 || tile == 5780 || tile == 5781 || tile == 5782 || tile == 5783 || tile == 5784 || tile == 5785 || tile == 5710 || tile == 5711 || tile == 5786 || tile == 5787 || tile == 5788 || tile == 5789 || tile == 5790 || tile == 5791 || tile == 6146 || tile == 6147 || tile == 6148 || tile == 6149 || tile == 6150 || tile == 6151 || tile == 6152 || tile == 6153 || tile == 5670 || tile == 5671 || tile == 5798 || tile == 5799 || tile == 5800 || tile == 5801 || tile == 5802 || tile == 5803 || tile == 5668 || tile == 5669 || tile == 5792 || tile == 5793 || tile == 5794 || tile == 5795 || tile == 5796 || tile == 5797 || tile == 544 || tile == 546 || tile == 4520 || tile == 382 || tile == 3116 || tile == 1792 || tile == 5666 || tile == 2994 || tile == 4368) return;
                if (tile == 1902 || tile == 1508 || tile == 428) return;
                if (tile == 1770 || tile == 4720 || tile == 4882 || tile == 6392 || tile == 3212 || tile == 1832 || tile == 4742 || tile == 3496 || tile == 3270 || tile == 4722 || tile == 6864) return;
                if (tile >= 7068) return;
                if (tile == 1280)
                {
                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"),
                    "set_default_color|`o\n\nadd_label_with_icon|big|`wChange your GrowID``|left|1280|\n\nadd_label|small|`oThis will change your GrowID `4permanently`o.You will pay `220,000$ `oif you click on `5Change it`o.``|left|1280|\n\nadd_text_input|newgrowid|Enter your new name:||15|\nend_dialog|changename|Cancel|Change it!"));
                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                    return;
                }
                if (tile == 18)
                {
                    if (world.items[x + (y * world.width)].background == 6864 && world.items[x + (y * world.width)].foreground == 0) return;
                    if (world.items[x + (y * world.width)].background == 0 && world.items[x + (y * world.width)].foreground == 0) return;
                    //data.netID = -1;
                    data.packetType = 0x8;
                    data.plantingTree = 4;
                    long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    //if (world->items[x + (y*world->width)].foreground == 0) return;
                    if (time - world.items[x + (y * world.width)].breakTime >= 4000)
                    {
                        world.items[x + (y * world.width)].breakTime = time;
                        world.items[x + (y * world.width)].breakLevel = 4; // TODO
                        if (world.items[x + (y * world.width)].foreground == 758)
                            sendRoulete(peer, x, y);
                    }
                    else
                        if (y < world.height && world.items[x + (y * world.width)].breakLevel + 4 >= def.breakHits * 4)
                    { // TODO
                        data.packetType = 0x3;// 0xC; // 0xF // World::HandlePacketTileChangeRequest
                        data.netID = -1;
                        data.plantingTree = 0;
                        world.items[x + (y * world.width)].breakLevel = 0;
                        int brokentile = world.items[x + (y * world.width)].foreground;

                        if (world.items[x + (y * world.width)].foreground != 0)
                        {
                            if (world.items[x + (y * world.width)].foreground == 242)
                            {
                                world.owner = "";
                                world.isPublic = false;
                                world.access = "";
                            }
                            if (world.items[x + (y * world.width)].foreground == 410)
                            {
                                if ((peer.Data as PlayerInfo).cpX / 32 == x && (peer.Data as PlayerInfo).cpY / 32 == y)
                                {
                                    WorldInfo info = getPlyersWorld(peer);
                                    int xo = 3040;
                                    int yo = 736;

                                    for (int j = 0; j < info.width * info.height; j++)
                                    {
                                        if (info.items[j].foreground == 6)
                                        {
                                            xo = (j % info.width) * 32;
                                            yo = (j / info.width) * 32;
                                        }
                                    }
                                    (peer.Data as PlayerInfo).cpX = xo;
                                    (peer.Data as PlayerInfo).cpY = yo;
                                }
                            }
                            if (world.items[x + (y * world.width)].foreground == 224)
                            {
                                world.items[x + (y * world.width)].iop = 0;
                            }
                            if (world.items[x + (y * world.width)].foreground == 20)
                            {
                                if (world.items[x + (y * world.width)].usedsign == 1)
                                {
                                    world.items[x + (y * world.width)].sign = "";
                                    world.items[x + (y * world.width)].usedsign = 0;
                                }
                            }
                            if (world.items[x + (y * world.width)].foreground == 12)
                            {
                                if (world.items[x + (y * world.width)].useddoor == 1)
                                {
                                    world.items[x + (y * world.width)].dtext = "";
                                    world.items[x + (y * world.width)].useddoor = 0;
                                    world.items[x + (y * world.width)].dest = "";
                                    world.items[x + (y * world.width)].did = "";
                                    world.items[x + (y * world.width)].iop = 0;
                                }
                            }
                            if (world.items[x + (y * world.width)].foreground == 2946)
                            {
                                if (world.items[x + (y * world.width)].dblock != 0)
                                {
                                    world.items[x + (y * world.width)].dblock = 0;
                                }
                            }
                            world.items[x + (y * world.width)].foreground = 0;
                            //GEM SYSTEM
                            Random rand = new Random();
                            int val = rand.Next(0, 37);
                            (peer.Data as PlayerInfo).gem = (peer.Data as PlayerInfo).gem + val;
                            GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnSetBux"), (peer.Data as PlayerInfo).gem));
                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                          /*  String[] mfm = { "1", "5", "10" };
                            Random r = new Random();
                            int idk = r.Next(0, 3);
                            string a = mfm[idk];
                            int pxa = Convert.ToInt32(a);
                            sendDrop(peer, -1, x * 32, y * 32, 112, pxa, 0);*/
                            //GEM SYSTEM
                        }
                        else
                        {
                            data.plantingTree = 6864;
                            world.items[x + (y * world.width)].background = 6864;
                        }

                    }
                    else
                            if (y < world.height)
                    {
                        world.items[x + (y * world.width)].breakTime = time;
                        world.items[x + (y * world.width)].breakLevel += 4; // TODO
                        if (world.items[x + (y * world.width)].foreground == 758)
                            sendRoulete(peer, x, y);
                    }

                }
                else
                {
                    for (int i = 0; i < (peer.Data as PlayerInfo).inventory.items.Length; i++)
                    {
                        if ((peer.Data as PlayerInfo).inventory.items[i].itemID == tile)
                        {
                            if ((uint)(peer.Data as PlayerInfo).inventory.items[i].itemCount > 1)
                            {
                                (peer.Data as PlayerInfo).inventory.items[i].itemCount--;
                            }
                            else
                            {
                                Array.Clear((peer.Data as PlayerInfo).inventory.items, i, 1);
                            }
                        }
                    }
                    if (def.blockType == BlockTypes.BACKGROUND)
                    {
                        world.items[x + (y * world.width)].background = (short)tile;
                    }
                    else
                    {
                        if (world.items[x + (y * world.width)].foreground != 0) return;
                        int xx = data.punchX;
                        int yy = data.punchY;
                        foreach (ENetPeer currentPeer in peers)
                        {
                            if (currentPeer.State != ENetPeerState.Connected)
                                continue;
                            if (isHere(peer, currentPeer))
                            {
                                if (tile == 1368 && (currentPeer.Data as PlayerInfo).x / 32 == xx && (currentPeer.Data as PlayerInfo).y / 32 == yy)
                                {
                                    int netid = (peer.Data as PlayerInfo).netID;
                                    int netid1 = (currentPeer.Data as PlayerInfo).netID;
                                    GamePacket p2x = packetEnd(appendInt(appendString(createPacket(), "OnSetFreezeState"), 0));
                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2x.data, 8, 4);
                                    int respawnTimeout = 3500;
                                    int deathFlag = 0x19;
                                    Array.Copy(BitConverter.GetBytes(respawnTimeout), 0, p2x.data, 24, 4);
                                    Array.Copy(BitConverter.GetBytes(deathFlag), 0, p2x.data, 56, 4);
                                    peer.Send(p2x.data, 0, ENetPacketFlags.Reliable);
                                    GamePacket pf = packetEnd(appendInt(appendString(createPacket(), "OnSetFreezeState"), 1));
                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, pf.data, 8, 4);
                                    peer.Send(pf.data, 0, ENetPacketFlags.Reliable);
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`oYou are frozen for 5 secounds"));
                                    currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                    sendtradeaff(peer, 1368, netid, netid1, 150);
                                    string text = "action|play_sfx\nfile|audio/freeze.wav\ndelayMS|0\n";
                                    byte[] data3 = new byte[5 + text.Length];
                                    int zero = 0;
                                    int type = 3;
                                    Array.Copy(BitConverter.GetBytes(type), 0, data3, 0, 4);
                                    Array.Copy(Encoding.ASCII.GetBytes(text), 0, data3, 4, text.Length);
                                    Array.Copy(BitConverter.GetBytes(zero), 0, data3, 4 + text.Length, 1);
                                    currentPeer.Send(data3, 0, ENetPacketFlags.Reliable);
                                    sendInventory(peer, (peer.Data as PlayerInfo).inventory);
                                    return;
                                }
                                else if (tile == 276 && (currentPeer.Data as PlayerInfo).x / 32 == xx && (currentPeer.Data as PlayerInfo).y / 32 == yy)
                                {
                                    int netid = (peer.Data as PlayerInfo).netID;
                                    int netid1 = (currentPeer.Data as PlayerInfo).netID;
                                    sendtradeaff(peer, 276, netid, netid1, 150);
                                    respawn(currentPeer, false);
                                    return;
                                }
                                else if ((currentPeer.Data as PlayerInfo).x / 32 == xx && (currentPeer.Data as PlayerInfo).y / 32 == yy) return;
                                else
                                {
                                    //dont allow to put consumbales
                                    if (tile == 1368) return;
                                    if (tile == 276) return;
                                }
                            }
                        }
                        world.items[x + (y * world.width)].foreground = (short)tile;
                        if (tile == 242)
                        {
                            world.owner = (peer.Data as PlayerInfo).rawName;
                            world.isPublic = false;

                            foreach (ENetPeer currentPeer in peers)
                            {
                                if (currentPeer.State != ENetPeerState.Connected)
                                    continue;
                                if (isHere(peer, currentPeer))
                                {
                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`3[`w" + world.name + " `ohas been World Locked by `2" + (peer.Data as PlayerInfo).displayName + "`3]"));

                                    currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                }
                            }
                        }

                    }

                    world.items[x + (y * world.width)].breakLevel = 0;
                }

                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                        SendPacketRaw(4, packPlayerMoving(data), 56, 0, currentPeer, 0);

                    //cout << "Tile update at: " << data2->punchX << "x" << data2->punchY << endl;
                }
            }
            catch
            {
                Console.WriteLine("error in void sendtileupdate");
            }
        }

        public static void sendPlayerLeave(ENetPeer peer, PlayerInfo player)
        {
            try
            {
                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnRemove"), "netID|" + player.netID + "\n")); // ((PlayerInfo*)(server->peers[i].data))->tankIDName
                GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`5<`w" + player.displayName + "`` left, `w" + getPlayersCountInWorld(player.currentWorld) + "`` others here>``"));

                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        {

                            currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);

                        }
                        {

                            currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in void sendplayerleave");
            }
        }

        public static void sendChatMessage(ENetPeer peer, int netID, string message)
        {
            try
            {
                if (message.Length == 0) return;
                string name = "";

                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if ((currentPeer.Data as PlayerInfo).netID == netID)
                        name = (currentPeer.Data as PlayerInfo).displayName;

                }
                foreach (char c in message)
                {
                    if (c < 0x18)
                    {
                        return;
                    }
                }
                string code = "";
                if ((peer.Data as PlayerInfo).adminLevel == 999)
                {
                    code = "`^";
                }
                else if ((peer.Data as PlayerInfo).adminLevel == 666)
                {
                    code = "`#";
                }
                else if ((peer.Data as PlayerInfo).adminLevel == 333)
                {
                    code = "`r";
                }
                else
                {
                    code = "`o";
                }
                if ((peer.Data as PlayerInfo).isMuted == 1)
                {
                    String[] mfm = { "mfmfmffmfmmf", "fmfmmmfmffmf", "mffmfmfmfmmff" };
                    Random r = new Random();
                    int idk = r.Next(0, 3);
                    message = mfm[idk];
                }
                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "CP:0_PL:4_OID:_CT:[W]_ `o<`w" + name + "`o> " + code + message));
                GamePacket p2 = packetEnd(appendIntx(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), netID), code + message), 0));

                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {

                        currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);

                        //enet_host_flush(server);

                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                        //enet_host_flush(server);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in void sendchatmessage");
            }
        }

        public static void sendWho(ENetPeer peer)
        {
            string name = "";

            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {
                    GamePacket p2 = packetEnd(appendIntx(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (currentPeer.Data as PlayerInfo).netID), (currentPeer.Data as PlayerInfo).displayName), 1));

                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                    //enet_host_flush(server);
                }
            }
        }



        public static void sendWorld(ENetPeer peer, WorldInfo worldInfo)
        {
            Console.WriteLine("Entering a world...");
            (peer.Data as PlayerInfo).joinClothesUpdated = false;
            string asdf = "0400000004A7379237BB2509E8E0EC04F8720B050000000000000000FBBB0000010000007D920100FDFDFDFD04000000040000000000000000000000070000000000"; // 0400000004A7379237BB2509E8E0EC04F8720B050000000000000000FBBB0000010000007D920100FDFDFDFD04000000040000000000000000000000080000000000000000000000000000000000000000000000000000000000000048133A0500000000BEBB0000070000000000
            string worldName = worldInfo.name;
            int xSize = worldInfo.width;
            int ySize = worldInfo.height;
            int square = xSize * ySize;
            Int16 nameLen = (Int16)worldName.Length;
            int payloadLen = asdf.Length / 2;
            int dataLen = payloadLen + 2 + nameLen + 12 + (square * 8) + 4 + 100;
            int offsetData = dataLen - 100;
            int allocMem = payloadLen + 2 + nameLen + 12 + (square * 8) + 4 + 16000 + 100;
            byte[] data = new byte[allocMem];
            for (int io = 0; io < allocMem; io++) data[io] = 0;
            for (int i = 0; i < asdf.Length; i += 2)
            {
                char x = (char)ch2n(asdf[i]);
                x = Convert.ToChar(x << 4);
                x += Convert.ToChar(ch2n(asdf[i + 1]));
                Array.Copy(BitConverter.GetBytes(x), 0, data, (i / 2), 1);
            }
            Int16 item = 0;
            int smth = 0;
            int zero = 0;
            for (int i = 0; i < square * 8; i += 4) Array.Copy(BitConverter.GetBytes(zero), 0, data, payloadLen + i + 14 + nameLen, 4);
            for (int i = 0; i < square * 8; i += 8) Array.Copy(BitConverter.GetBytes(item), 0, data, payloadLen + i + 14 + nameLen, 2);
            Array.Copy(BitConverter.GetBytes(nameLen), 0, data, payloadLen, 2);
            Array.Copy(Encoding.ASCII.GetBytes(worldName), 0, data, payloadLen + 2, nameLen);
            Array.Copy(BitConverter.GetBytes(xSize), 0, data, payloadLen + 2 + nameLen, 4);
            Array.Copy(BitConverter.GetBytes(ySize), 0, data, payloadLen + 6 + nameLen, 4);
            Array.Copy(BitConverter.GetBytes(square), 0, data, payloadLen + 10 + nameLen, 4);
            int blockPtr = payloadLen + 14 + nameLen;
            int sizeofblockstruct = 8;

            for (int i = 0; i < square; i++)
            {
                int tile = worldInfo.items[i].foreground;
                sizeofblockstruct = 8;
                if ((worldInfo.items[i].foreground == 0) || (worldInfo.items[i].foreground == 2) || (worldInfo.items[i].foreground == 8) || (worldInfo.items[i].foreground == 100))
                {
                    Array.Copy(BitConverter.GetBytes(worldInfo.items[i].foreground), 0, data, blockPtr, 2);
                    long type = 0x00000000;
                    // type 1 = locked
                    if (worldInfo.items[i].water)
                        type |= 0x04000000;
                    if (worldInfo.items[i].glue)
                        type |= 0x08000000;
                    if (worldInfo.items[i].fire)
                        type |= 0x10000000;
                    if (worldInfo.items[i].red)
                        type |= 0x20000000;
                    if (worldInfo.items[i].green)
                        type |= 0x40000000;
                    if (worldInfo.items[i].blue)
                        type |= 0x80000000;

                    Array.Copy(BitConverter.GetBytes(type), 0, data, blockPtr + 4, 4);
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes(zero), 0, data, blockPtr, 2);
                }
                Array.Copy(BitConverter.GetBytes(worldInfo.items[i].background), 0, data, blockPtr + 2, 2);
                blockPtr += sizeofblockstruct;
            }
            offsetData = dataLen - 100;
            string asdf2 = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            byte[] data2 = new byte[101];
            for (int i = 0; i < asdf2.Length; i += 2)
            {
                char x = (char)ch2n(asdf2[i]);
                x = Convert.ToChar(x << 4);
                x += Convert.ToChar(ch2n(asdf2[i + 1]));
                Array.Copy(BitConverter.GetBytes(x), 0, data2, (i / 2), 1);
            }
            int weather = worldInfo.weather;
            Array.Copy(BitConverter.GetBytes(weather), 0, data2, 4, 4);
            Array.Copy(data2, 0, data, offsetData, 100);
            Array.Copy(BitConverter.GetBytes(smth), 0, data, dataLen - 4, 4);
            peer.Send(data, 0, ENetPacketFlags.Reliable);
            for (int i = 0; i < square; i++)
            {
                if ((worldInfo.items[i].foreground == 0) || (worldInfo.items[i].foreground == 2) || (worldInfo.items[i].foreground == 8) || (worldInfo.items[i].foreground == 100))
                    ; // nothing
                else if (worldInfo.items[i].foreground == 6) updatedoor(peer, worldInfo.items[i].foreground, i % worldInfo.width, i / worldInfo.width, "`wEXIT``");
                else if (worldInfo.items[i].foreground == 3832) updatestuffweather(peer, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].stuffwe, worldInfo.items[i].background, worldInfo.items[i].gravity, false, false);
                else if (worldInfo.items[i].usedsign == 1) updatesign(peer, worldInfo.items[i].foreground, worldInfo.items[i].background, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].sign);
                else if (worldInfo.items[i].useddoor == 1)
                {
                    if (worldInfo.items[i].iop == 1)
                    {
                        updatedoor(peer, worldInfo.items[i].foreground, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].dtext);
                    }
                    else
                    {
                        string haveaccess1 = worldInfo.access;
                        string access1 = "";
                        foreach (string line in haveaccess1.Split(",".ToCharArray()))
                        {
                            string[] ex = explode("|", line);
                            string idk = ex[0];

                            access1 += idk;
                        }
                        if ((peer.Data as PlayerInfo).rawName == worldInfo.owner || (peer.Data as PlayerInfo).rawName == access1 || (peer.Data as PlayerInfo).adminLevel >= 666)
                        {
                            updatedoor(peer, worldInfo.items[i].foreground, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].dtext);
                        }
                        else
                        {
                            lockdoor(peer, worldInfo.items[i].foreground, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].dtext);
                        }
                    }
                }
                else if (worldInfo.items[i].foreground == 2946 && worldInfo.items[i].dblock != 0) updatedisplayblock(peer, worldInfo.items[i].foreground, i % worldInfo.width, i / worldInfo.width, worldInfo.items[i].background, worldInfo.items[i].dblock);
                else
                {
                    PlayerMoving data1 = new PlayerMoving();
                    //data.packetType = 0x14;
                    data1.packetType = 0x3;

                    //data.characterState = 0x924; // animation
                    data1.characterState = 0x0; // animation
                    data1.x = i % worldInfo.width;
                    data1.y = i / worldInfo.height;
                    data1.punchX = i % worldInfo.width;
                    data1.punchY = i / worldInfo.width;
                    data1.XSpeed = 0;
                    data1.YSpeed = 0;
                    data1.netID = -1;
                    data1.plantingTree = worldInfo.items[i].foreground;
                    SendPacketRaw(4, packPlayerMoving(data1), 56, 0, peer, 0);
                }
            }
            WorldInfo info = worldDB.get2(worldName).info;
            int xo = 0;
            int yo = 0;
            for (int i = 0; i < info.dropcount; i++)
            {
                int k = i + 1;
                string s = k.ToString();
                for (int j = 0; j < info.width * info.height; j++)
                {
                    if (info.items[j].useddrop == 1)
                    {
                        if (info.items[j].uid == k)
                        {
                            xo = (j % info.width) * 32;
                            yo = (j / info.width) * 32;
                            string[] ex = explode("|", info.items[j].drop);
                            int idd = Convert.ToInt32(ex[1]);
                            int amo = Convert.ToInt32(ex[2]);
                            senddroppeer(peer, -1, xo, yo, idd, amo, 0);
                        }
                    }
                }
            }


            (peer.Data as PlayerInfo).currentWorld = worldInfo.name;

        }

        public static void sendAction(ENetPeer peer, int netID, string action)
        {
            string name = "";
            GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnAction"), action));

            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {

                    Array.Copy(BitConverter.GetBytes(netID), 0, p2.data, 8, 4);

                    currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                    //enet_host_flush(server);
                }
            }
        }

        // droping items WorldObjectMap::HandlePacket
        public static void sendDrop(ENetPeer peer, int netID, int x, int y, int item, int count, byte specialEffect)
        {
            if (item >= 9000) return;
            if (item < 0) return;
            string name = "";

            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {
                    PlayerMoving data = new PlayerMoving();
                    data.packetType = 14;
                    data.x = x;
                    data.y = y;
                    data.netID = netID;
                    data.plantingTree = item;
                    float val = count; // item count
                    byte val2 = specialEffect;

                    byte[] raw = packPlayerMoving(data);
                    Array.Copy(BitConverter.GetBytes(val), 0, raw, 16, 4);
                    Array.Copy(BitConverter.GetBytes(val2), 0, raw, 1, 1);

                    SendPacketRaw(4, raw, 56, 0, currentPeer, 0);
                }
            }
        }

        public static void senddroppeer(ENetPeer peer, int netID, int x, int y, int item, int count, byte specialEffect)
        {
            if (item >= 9000) return;
            if (item < 0) return;
            PlayerMoving data = new PlayerMoving();
            data.packetType = 14;
            data.x = x;
            data.y = y;
            data.netID = netID;
            data.plantingTree = item;
            float val = count; // item count
            byte val2 = specialEffect;

            byte[] raw = packPlayerMoving(data);
            Array.Copy(BitConverter.GetBytes(val), 0, raw, 16, 4);
            Array.Copy(BitConverter.GetBytes(val2), 0, raw, 1, 1);

            SendPacketRaw(4, raw, 56, 0, peer, 0);
        }

        public static void sendtake(ENetPeer peer, int netID, int x, int y, int item)
        {

            foreach (ENetPeer currentPeer in peers)
            {
                if (currentPeer.State != ENetPeerState.Connected)
                    continue;
                if (isHere(peer, currentPeer))
                {
                    PlayerMoving data = new PlayerMoving();
                    data.packetType = 14;
                    data.x = x;
                    data.y = y;
                    data.netID = netID;
                    data.plantingTree = item;
                    byte[] raw = packPlayerMoving(data);
                    SendPacketRaw(4, raw, 56, 0, currentPeer, 0);
                }
            }
        }

        public static void sendState(ENetPeer peer)
        {
            try
            {
                //return; // TODO
                PlayerInfo info = peer.Data as PlayerInfo;
                int netID = info.netID;
                int state = getState(info);

                foreach (ENetPeer currentPeer in peers)
                {
                    if (currentPeer.State != ENetPeerState.Connected)
                        continue;
                    if (isHere(peer, currentPeer))
                    {
                        PlayerMoving data;
                        data.packetType = 0x14;
                        data.characterState = 0; // animation
                        data.x = 1000;
                        data.y = 100;
                        data.punchX = 0;
                        data.punchY = 0;
                        data.XSpeed = 300;
                        data.YSpeed = 600;
                        data.netID = netID;
                        data.plantingTree = state;
                        byte[] raw = packPlayerMoving(data);
                        int var = 0x808000; // placing and breking
                        Array.Copy(BitConverter.GetBytes(var), 0, raw, 1, 3);
                        SendPacketRaw(4, raw, 56, 0, currentPeer, 0);
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in void sendstate");
            }
            // TODO
        }

        public static void sendWorldOffers(ENetPeer peer)
        {
            if (!(peer.Data as PlayerInfo).isIn) return;
            WorldInfo[] worldz = worldDB.getRandomWorlds();
            string worldOffers = "default|";
            if (worldz.Length > 0)
            {
                worldOffers += worldz[0].name;
            }

            worldOffers += "\nadd_button|Showing: `wWorlds``|_catselect_|0.6|3529161471|\n";
            for (int i = 0; i < worldz.Length; i++)
            {
                worldOffers += "add_floater|" + worldz[i].name + "|" + getPlayersCountInWorld(worldz[i].name) + "|0.55|3529161471\n";
            }
            GamePacket p3 = packetEnd(appendString(appendString(createPacket(), "OnRequestWorldSelectMenu"), worldOffers));
            peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
        }

        public static void HandlerRoutine(object sender, ConsoleCancelEventArgs e)
        {
            saveAllWorlds();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Project Reborn in c#");
                ManagedENet.Startup();
                IntializeDB();

                Console.CancelKeyPress += HandlerRoutine;
                // load items.dat
                if (File.Exists("items.dat"))
                {
                    byte[] itemsData = File.ReadAllBytes("items.dat");
                    itemsDatSize = itemsData.Length;

                    itemsDat = new byte[60 + itemsDatSize];
                    string asdf = "0400000010000000FFFFFFFF000000000800000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                    for (int i = 0; i < asdf.Length; i += 2)
                    {
                        byte x = ch2n(asdf[i]);
                        x = (byte)(x << 4);
                        x += ch2n(asdf[i + 1]);
                        itemsDat[i / 2] = x;
                        if (asdf.Length > 60 * 2) throw new Exception("Error");
                    }
                    Array.Copy(BitConverter.GetBytes(itemsDatSize), 0, itemsDat, 56, 4);

                    byte[] itData = File.ReadAllBytes("items.dat");

                    string kkb = BitConverter.ToString(itemsDat);
                    kkb = kkb.Replace("-", "");

                    if (kkb.Length > 60 || kkb.Length == 60)
                    {
                        kkb = kkb.Substring(0, 120);
                        string kkc = BitConverter.ToString(itData);
                        kkc = kkc.Replace("-", "");
                        string bitreal = kkb + kkc;
                        byte[] abcf = FromHex(bitreal);
                        // Console.WriteLine(bitreal);
                        itemsDat = abcf;
                    }



                    Console.WriteLine("Updating item data success!");
                }
                else
                {
                    Console.WriteLine("Updating item data failed");
                }

                worldDB = new WorldDB();

                //world = generateWorld();
                worldDB.get("TEST");
                worldDB.get("MAIN");
                worldDB.get("NEW");
                worldDB.get("ADMIN");
                IPEndPoint address = new IPEndPoint(IPAddress.Any, 17091);

                server = new ENetHost(address, 1024, 10);
                server.ChecksumWithCRC32();
                server.CompressWithRangeCoder();

                Console.WriteLine("Building items database...");
                buildItemsDatabase();
                Console.WriteLine("Database is built!");

                server.OnConnect += (object sender, ENetConnectEventArgs eve) =>
                {
                    {
                        ENetPeer peer = eve.Peer;
                        Console.WriteLine("A new client connected.");
                    //event.peer->data = "Client information";
                    int count = 0;

                        foreach (ENetPeer currentPeer in peers)
                        {
                            if (currentPeer.State != ENetPeerState.Connected)
                                continue;
                            if (currentPeer.RemoteEndPoint.Equals(peer.RemoteEndPoint))
                                count++;
                        }

                        peer.Data = new PlayerInfo();
                        if (count > 3)
                        {
                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
                                "`rToo many accounts are logged on from this IP. Log off one account before playing please.``"));
                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                        //enet_host_flush(server);
                        peer.DisconnectLater(0);
                        }
                        else
                        {
                            sendData(peer, 1, BitConverter.GetBytes(0), 0);
                            peers.Add(peer);
                        }
                    }

                    eve.Peer.OnReceive += (object send, ENetPacket ev) =>
                    {
                        byte[] pak = ev.GetPayloadCopy();
                        ENetPeer peer = send as ENetPeer;
                        if (peer.State != ENetPeerState.Connected) return;
                        if (peer == null) return;
                        if ((peer.Data as PlayerInfo).isUpdating)
                        {
                            Console.WriteLine("packet drop");
                            return;
                        }

                        int messageType = pak[0];
                    //cout << "Packet type is " << messageType << endl;
                    //cout << (event->packet->data+4) << endl;
                    WorldInfo world = getPlyersWorld(peer);
                        switch (messageType)
                        {
                            case 2:
                                {
                                    //cout << GetTextPointerFromPacket(event.packet) << endl;
                                    long timezx = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                    string cch = Encoding.ASCII.GetString(pak.Take(pak.Length - 1).Skip(4).ToArray());
                                    if ((peer.Data as PlayerInfo).lastent1 + 350 < timezx)
                                    {
                                        (peer.Data as PlayerInfo).lastent1 = timezx;
                                        goto outt;
                                    }
                                    else
                                    {
                                        if (cch == "action|refresh_item_data\n")
                                        {

                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    outt:;
                                    if (cch.IndexOf("action|respawn") == 0)
                                    {
                                        try
                                        {
                                            if (cch.IndexOf("action|respawn_spike") == 0)
                                            {
                                                respawn(peer, true);
                                            }
                                            else
                                            {
                                                respawn(peer, false);
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|respawn");
                                        }
                                    }


                                    if (cch.IndexOf("action|growid") == 0)
                                    {
                                        try
                                        {
                                            //GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`w" + itemDefs.at(id).name + "``|left|" + std::to_string(id) + "|\n\nadd_spacer|small|\nadd_textbox|" + itemDefs.at(id).description + "|left|\nadd_spacer|small|\nadd_quick_exit|\nadd_button|chc0|Close|noflags|0|0|\nnend_dialog|gazette||OK|"));
                                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"),
                                                    "set_default_color|`o\n\nadd_label_with_icon|big|`wGet a GrowID``|left|206|\n\nadd_spacer|small|\nadd_textbox|A `wGrowID `wmeans `oyou can use a name and password to logon from any device.|\nadd_spacer|small|\nadd_textbox|This `wname `owill be reserved for you and `wshown to other players`o, so choose carefully!|\nadd_text_input|username|GrowID||30|\nadd_text_input|password|Password||100|\nadd_text_input|passwordverify|Password Verify||100|\nadd_textbox|Your `wemail address `owill only be used for account verification purposes and won't be spammed or shared. If you use a fake email, you'll never be able to recover or change your password.|\nadd_text_input|email|Email||100|\nadd_textbox|Your `wDiscord ID `owill be used for secondary verification if you lost access to your `wemail address`o! Please enter in such format: `wdiscordname#tag`o. Your `wDiscord Tag `ocan be found in your `wDiscord account settings`o.|\nadd_text_input|discord|Discord||100|\nend_dialog|register|Cancel|Get My GrowID!|\n"));
                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|growid");
                                        }
                                    }

                                    if (cch.IndexOf("action|store") == 0)
                                    {
                                        try
                                        {
                                            GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnStoreRequest"),
                                                "set_description_text|Welcome to the `2Growtopia Store``!  Tap the item you'd like more info on.`o  `wWant to get `5Supporter`` status? Any Gem purchase (or `57,000`` Gems earned with free `5Tapjoy`` offers) will make you one. You'll get new skin colors, the `5Recycle`` tool to convert unwanted items into Gems, and more bonuses!\nadd_button|iap_menu|Buy Gems|interface/large/store_buttons5.rttex||0|2|0|0||\nadd_button|subs_menu|Subscriptions|interface/large/store_buttons22.rttex||0|1|0|0||\nadd_button|token_menu|Growtoken Items|interface/large/store_buttons9.rttex||0|0|0|0||\nadd_button|pristine_forceps|`oAnomalizing Pristine Bonesaw``|interface/large/store_buttons20.rttex|Built to exacting specifications by GrowTech engineers to find and remove temporal anomalies from infected patients, and with even more power than Delicate versions! Note : The fragile anomaly - seeking circuitry in these devices is prone to failure and may break (though with less of a chance than a Delicate version)! Use with care!|0|3|3500|0||\nadd_button|itemomonth|`oItem Of The Month``|interface/large/store_buttons16.rttex|`2September 2018:`` `9Sorcerer's Tunic of Mystery!`` Capable of reflecting the true colors of the world around it, this rare tunic is made of captured starlight and aether. If you think knitting with thread is hard, just try doing it with moonbeams and magic! The result is worth it though, as these clothes won't just make you look amazing - you'll be able to channel their inherent power into blasts of cosmic energy!``|0|3|200000|0||\nadd_button|contact_lenses|`oContact Lens Pack``|interface/large/store_buttons22.rttex|Need a colorful new look? This pack includes 10 random Contact Lens colors (and may include Contact Lens Cleaning Solution, to return to your natural eye color)!|0|7|15000|0||\nadd_button|locks_menu|Locks And Stuff|interface/large/store_buttons3.rttex||0|4|0|0||\nadd_button|itempack_menu|Item Packs|interface/large/store_buttons3.rttex||0|3|0|0||\nadd_button|bigitems_menu|Awesome Items|interface/large/store_buttons4.rttex||0|6|0|0||\nadd_button|weather_menu|Weather Machines|interface/large/store_buttons5.rttex|Tired of the same sunny sky?  We offer alternatives within...|0|4|0|0||\n"));
                                            peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                            //enet_host_flush(server);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|store");
                                        }
                                    }

                                    if (cch.IndexOf("action|info") == 0)
                                    {
                                        try
                                        {
                                            int id = -1;
                                            int count = -1;
                                            foreach (string to in cch.Split("\n".ToCharArray()))
                                            {
                                                string[] infoDat = explode("|", to);
                                                if (infoDat.Length == 3)
                                                {
                                                    if (infoDat[1] == "itemID")
                                                    {
                                                        int a = Convert.ToInt32(infoDat[2]);
                                                        if (a > 9279)
                                                        {
                                                            return;
                                                            //thanks for raiter for the fix
                                                        }
                                                        else if (a < 0)
                                                        {
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            id = a;
                                                        }
                                                    }
                                                    if (infoDat[1] == "count")
                                                    {
                                                        int a1 = Convert.ToInt32(infoDat[2]);
                                                        if (a1 > 200)
                                                        {
                                                            return;
                                                        }
                                                        else if (a1 < 200)
                                                        {
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            count = a1;
                                                        }
                                                    }
                                                }
                                            }

                                            if (id == -1 || count == -1) return;
                                            if (itemDefs.Length < id || id < 0) return;
                                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"),
                                                "set_default_color|`o\n\nadd_label_with_icon|big|`w" + itemDefs[id].name +
                                                "``|left|" + id + "|\n\nadd_spacer|small|\nadd_textbox|" +
                                                itemDefs[id].description +
                                                "|left|\nadd_spacer|small|\nadd_quick_exit|\nadd_button|chc0|Close|noflags|0|0|\nnend_dialog|gazette||OK|"));
                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                            //enet_host_flush(server);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|info");
                                        }

                                    }

                                    if (cch.IndexOf("action|dialog_return") == 0)
                                    {
                                        try
                                        {
                                            string btn = "";
                                            bool isRegisterDialog = false;
                                            bool isFindDialog = false;
                                            bool isStuffDialog = false;
                                            bool isSignDialog = false;
                                            bool isvenddialog = false;
                                            bool isDoorDialog = false;
                                            bool addaccess = false;
                                            bool dropdialog = false;
                                            bool istradedialog = false;
                                            bool isvendpicker = false;
                                            bool changenamedialog = false;
                                            bool emtyvend = false;
                                            string item1count = "";
                                            string item2count = "";
                                            string item3count = "";
                                            string item4count = "";
                                            string netidss = "";
                                            string stuffitem = "";
                                            string gravitystr = "";
                                            string itemFind = "";
                                            string username = "";
                                            string password = "";
                                            string passwordverify = "";
                                            string email = "";
                                            string discord = "";
                                            string signContent = "";
                                            string dtext = "";
                                            string dest = "";
                                            string did = "";
                                            string amount = "";
                                            string iteminvend = "";
                                            string itemprice = "";
                                            string newname = "";
                                            foreach (string to in cch.Split("\n".ToCharArray()))
                                            {
                                                string[] infoDat = explode("|", to);
                                                if (infoDat.Length == 2)
                                                {
                                                    if (infoDat[0] == "buttonClicked") btn = infoDat[1];
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "register")
                                                    {
                                                        isRegisterDialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "changename")
                                                    {
                                                        changenamedialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "findid")
                                                    {
                                                        isFindDialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "dropdialog")
                                                    {
                                                        dropdialog = true;
                                                    }
                                                    if (infoDat[0] == "stuffitem")
                                                    {
                                                        isStuffDialog = true;

                                                    }
                                                    if (infoDat[0] == "netid")
                                                    {
                                                        netidss = infoDat[1];
                                                        addaccess = true;

                                                    }
                                                    if (infoDat[0] == "isWorldPublic" && infoDat[1] == "1")
                                                    {

                                                        if ((peer.Data as PlayerInfo).rawName == getPlyersWorld(peer).owner)

                                                            getPlyersWorld(peer).isPublic = true;
                                                    }
                                                    if (infoDat[0] == "isWorldPublic" && infoDat[1] == "0")
                                                    {
                                                        if ((peer.Data as PlayerInfo).rawName == getPlyersWorld(peer).owner)
                                                            getPlyersWorld(peer).isPublic = false;
                                                    }
                                                    if (infoDat[0] == "ishpub" && infoDat[1] == "1")
                                                    {
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int bg = world.items[x + (y * world.width)].background;
                                                        world.items[x + (y * world.width)].iop = 1;
                                                        updateEntrance(peer, fg, x, y, true, bg);
                                                    }
                                                    if (infoDat[0] == "ishpub" && infoDat[1] == "0")
                                                    {
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int bg = world.items[x + (y * world.width)].background;
                                                        world.items[x + (y * world.width)].iop = 0;
                                                        updateEntrance(peer, fg, x, y, false, bg);
                                                    }
                                                    if (infoDat[0] == "isopenpublic" && infoDat[1] == "1")
                                                    {
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        world.items[x + (y * world.width)].iop = 1;
                                                    }
                                                    if (infoDat[0] == "isopenpublic" && infoDat[1] == "0")
                                                    {
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        world.items[x + (y * world.width)].iop = 0;
                                                    }

                                                    if (infoDat[0] == "iteminvend")
                                                    {
                                                        isvendpicker = true;
                                                        if (infoDat[0] == "iteminvend") iteminvend = infoDat[1];
                                                    }

                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "signok")
                                                    {
                                                        isSignDialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "vendok")
                                                    {
                                                        isvenddialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "editdoor")
                                                    {
                                                        isDoorDialog = true;
                                                    }
                                                    if (infoDat[0] == "dialog_name" && infoDat[1] == "tradedialog")
                                                    {
                                                        istradedialog = true;
                                                    }

                                                    if (isSignDialog)
                                                    {
                                                        if (infoDat[0] == "sign") signContent = infoDat[1];
                                                    }

                                                    if (isvenddialog)
                                                    {
                                                        if (infoDat[0] == "sign") itemprice = infoDat[1];
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int oo = world.items[x + (y * world.width)].invend;
                                                        string itemname = itemDefs[oo].name;
                                                        int price = 0;
                                                        string[] gravitynum = Regex.Split(itemprice, @"\D+");
                                                        foreach (string value in gravitynum)
                                                        {
                                                            if (!string.IsNullOrEmpty(value))
                                                            {
                                                                int i = int.Parse(value);
                                                                price = i;
                                                            }
                                                        }
                                                        if (price < 0) return;
                                                        if (price > 200) return;
                                                        world.items[x + (y * world.width)].price = price;
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (isHere(peer, currentPeer))
                                                            {

                                                                if (price == 0)
                                                                {
                                                                    // price is 0 which mean its disabled already
                                                                    updatevend(currentPeer, x, y, oo, false, 0);
                                                                    GamePacket p3o = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`7[`w" + (peer.Data as PlayerInfo).displayName + " `wdisabled this Vending Machine.`7]"));
                                                                    currentPeer.Send(p3o.data, 0, ENetPacketFlags.Reliable);

                                                                }
                                                                else
                                                                {
                                                                    // to update price
                                                                    updatevend(currentPeer, x, y, oo, false, price);
                                                                    GamePacket p3 = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`7[`w" + (peer.Data as PlayerInfo).displayName + " `wchanged the price of `2" + itemname + "`w to `5" + price + " World Locks each.`7]"));
                                                                    currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (dropdialog)
                                                    {
                                                        if (infoDat[0] == "amount") amount = infoDat[1];
                                                    }
                                                    if (isDoorDialog)
                                                    {
                                                        if (infoDat[0] == "label") dtext = infoDat[1];
                                                        if (infoDat[0] == "dest") dest = infoDat[1];
                                                        if (infoDat[0] == "doorid") did = infoDat[1];
                                                    }
                                                    if (isRegisterDialog)
                                                    {
                                                        if (infoDat[0] == "username") username = infoDat[1];
                                                        if (infoDat[0] == "password") password = infoDat[1];
                                                        if (infoDat[0] == "passwordverify") passwordverify = infoDat[1];
                                                        if (infoDat[0] == "email") email = infoDat[1];
                                                        if (infoDat[0] == "discord") discord = infoDat[1];
                                                    }
                                                    if (changenamedialog)
                                                    {
                                                        if (infoDat[0] == "newgrowid") newname = infoDat[1];
                                                    }
                                                    if (isFindDialog)
                                                    {
                                                        if (infoDat[0] == "item") itemFind = infoDat[1];
                                                    }

                                                    if (istradedialog)
                                                    {
                                                        if (infoDat[0] == "itemcount") item1count = infoDat[1];
                                                        if (infoDat[0] == "itemcount2") item2count = infoDat[1];
                                                        if (infoDat[0] == "itemcount3") item3count = infoDat[1];
                                                        if (infoDat[0] == "itemcount4") item4count = infoDat[1];
                                                    }

                                                    if (isvendpicker)
                                                    {
                                                        int oo = Convert.ToInt32(iteminvend);
                                                        int x = (peer.Data as PlayerInfo).lastPunchX;
                                                        int y = (peer.Data as PlayerInfo).lastPunchY;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int bg = world.items[x + (y * world.width)].background;
                                                        string itemname = itemDefs[oo].name;
                                                        world.items[x + (y * world.width)].invend = oo;
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (isHere(peer, currentPeer))
                                                            {
                                                                updatevend(currentPeer, x, y, oo, false, 0);
                                                                GamePacket p3 = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`7[`w" + (peer.Data as PlayerInfo).displayName + " `wput `2" + itemname + "`w in the Vending Machine.`7]"));
                                                                currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                        GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wVending Machine``|left|2978|\nadd_spacer|small|\nadd_label_with_icon|small|`oThe machine contains 1 `2" + itemname + "|left|" + oo + "|\nadd_spacer|small|\nadd_label|small|Not currently for sale|\nadd_button|emptyvend|Empty the machine|\nadd_smalltext|`5(Vending Machine will not function when price is set to 0)|\nadd_text_input|sign|Price ||6||\nadd_quick_exit|\nend_dialog|vendok|Close|Update"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    }

                                                    if (isStuffDialog)
                                                    {
                                                        if ((peer.Data as PlayerInfo).currentWorld != "EXIT")
                                                        {
                                                            int stuffitemi = -1;
                                                            int gravity = 100;

                                                            int x = (peer.Data as PlayerInfo).lastPunchX;
                                                            int y = (peer.Data as PlayerInfo).lastPunchY;

                                                            if (infoDat[0] == "stuffitem") stuffitem = infoDat[1];
                                                            if (infoDat[0] == "gravity") gravitystr = infoDat[1];
                                                            if (gravitystr.Contains(" ")) return;
                                                            string[] gravitynum = Regex.Split(gravitystr, @"\D+");
                                                            foreach (string value in gravitynum)
                                                            {
                                                                if (!string.IsNullOrEmpty(value))
                                                                {
                                                                    int i = int.Parse(value);
                                                                    gravity = i;
                                                                }
                                                            }
                                                            int Number;
                                                            bool isNumber;
                                                            isNumber = Int32.TryParse(stuffitem, out Number);

                                                            if (!isNumber)
                                                            {
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                stuffitemi = Convert.ToInt32(stuffitem);
                                                            }


                                                            if (gravity > -1000 && gravity < 1000 && stuffitemi > -1 && stuffitemi < 9142)
                                                            {
                                                                world.items[x + (y * world.width)].stuffwe = stuffitemi;
                                                                world.items[x + (y * world.width)].gravity = gravity;
                                                            }

                                                            updatestuffweather(peer, x, y, stuffitemi, world.items[x + (y * world.width)].background, gravity, false, false);
                                                            getPlyersWorld(peer).weather = 29;
                                                        }

                                                    }
                                                }
                                            }

                                            if (istradedialog)
                                            {
                                                int x = 0;
                                                if (item1count != "")
                                                {
                                                    int Number;
                                                    bool isNumber;
                                                    isNumber = Int32.TryParse(item1count, out Number);

                                                    if (!isNumber)
                                                    {
                                                        return;
                                                    }
                                                    x = Convert.ToInt32(item1count);
                                                    if (x > 200)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                         "`oyou cant trade more than 200!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    if (x < 0)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`oyou cant trade less than 0!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    (peer.Data as PlayerInfo).item1count = x;
                                                }
                                                if (item2count != "")
                                                {
                                                    int Number;
                                                    bool isNumber;
                                                    isNumber = Int32.TryParse(item2count, out Number);

                                                    if (!isNumber)
                                                    {
                                                        return;
                                                    }
                                                    x = Convert.ToInt32(item2count);
                                                    if (x > 200)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                         "`oyou cant trade more than 200!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    if (x < 0)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`oyou cant trade less than 0!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    (peer.Data as PlayerInfo).item2count = x;
                                                }
                                                if (item3count != "")
                                                {
                                                    int Number;
                                                    bool isNumber;
                                                    isNumber = Int32.TryParse(item3count, out Number);

                                                    if (!isNumber)
                                                    {
                                                        return;
                                                    }
                                                    x = Convert.ToInt32(item3count);
                                                    if (x > 200)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`oyou cant trade more than 200!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    if (x < 0)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`oyou cant trade less than 0!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    (peer.Data as PlayerInfo).item3count = x;
                                                }
                                                if (item4count != "")
                                                {
                                                    int Number;
                                                    bool isNumber;
                                                    isNumber = Int32.TryParse(item4count, out Number);

                                                    if (!isNumber)
                                                    {
                                                        return;
                                                    }
                                                    x = Convert.ToInt32(item4count);
                                                    if (x > 200)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                            "`oyou cant trade more than 200!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    if (x < 0)
                                                    {
                                                        GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`oyou cant trade less than 0!``"));
                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    (peer.Data as PlayerInfo).item4count = x;
                                                }
                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    if (!(peer.Data as PlayerInfo).radio)
                                                        continue;

                                                    tradestatus(peer, (currentPeer.Data as PlayerInfo).netID, "", (currentPeer.Data as PlayerInfo).displayName, "add_slot|" + (currentPeer.Data as PlayerInfo).item1 + "|" + (currentPeer.Data as PlayerInfo).item1count + "\nadd_slot|" + (currentPeer.Data as PlayerInfo).item2 + "|" + (currentPeer.Data as PlayerInfo).item2count + "\nadd_slot|" + (currentPeer.Data as PlayerInfo).item3 + "|" + (currentPeer.Data as PlayerInfo).item3count + "\nadd_slot|" + (currentPeer.Data as PlayerInfo).item4 + "|" + (currentPeer.Data as PlayerInfo).item4count);
                                                    tradestatus(currentPeer, (peer.Data as PlayerInfo).netID, "", (peer.Data as PlayerInfo).displayName, "add_slot|" + (peer.Data as PlayerInfo).item1 + "|" + (peer.Data as PlayerInfo).item1count + "\nadd_slot|" + (peer.Data as PlayerInfo).item2 + "|" + (peer.Data as PlayerInfo).item2count + "\nadd_slot|" + (peer.Data as PlayerInfo).item3 + "|" + (peer.Data as PlayerInfo).item3count + "\nadd_slot|" + (peer.Data as PlayerInfo).item4 + "|" + (peer.Data as PlayerInfo).item4count);
                                                }
                                            }

                                            if (dropdialog)
                                            {
                                            /*    int x = 0;
                                                int Number;
                                                bool isNumber;
                                                isNumber = Int32.TryParse(amount, out Number);

                                                if (!isNumber)
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    x = Convert.ToInt32(amount);
                                                }
                                                if (x < 0)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                     appendString(createPacket(), "OnConsoleMessage"),
                                                    "`oyou cant drop less than 0!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    return;
                                                }
                                                if (x > 200)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                     appendString(createPacket(), "OnConsoleMessage"),
                                                     "`oyou cant drop more than 200!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    return;
                                                }
                                                sendDrop(peer, -1, (peer.Data as PlayerInfo).x + (32 * ((peer.Data as PlayerInfo).isRotatedLeft ? -1 : 1)), (peer.Data as PlayerInfo).y, (peer.Data as PlayerInfo).dropid, x, 0);
                                                int a = (peer.Data as PlayerInfo).x + (32 * ((peer.Data as PlayerInfo).isRotatedLeft ? -1 : 1));
                                                int x1 = a / 32;
                                                int y1 = (peer.Data as PlayerInfo).y / 32;
                                                world.dropcount = world.dropcount + 1;
                                                if (world.items[x1 + (y1 * world.width)].drop == "")
                                                {
                                                    world.items[x1 + (y1 * world.width)].drop = world.dropcount + "|" + (peer.Data as PlayerInfo).dropid + "|" + x + "|,";
                                                    world.items[x1 + (y1 * world.width)].useddrop = 1;
                                                    world.items[x1 + (y1 * world.width)].uid = world.dropcount + "|,";
                                                }
                                                else
                                                {
                                                    world.items[x1 + (y1 * world.width)].drop = world.items[x1 + (y1 * world.width)].drop + world.dropcount + "|" + (peer.Data as PlayerInfo).dropid + "|" + x + "|,";
                                                    world.items[x1 + (y1 * world.width)].useddrop = 1;
                                                    world.items[x1 + (y1 * world.width)].uid = world.items[x1 + (y1 * world.width)].uid + world.dropcount + "|,";
                                                }*/
                                            }

                                            if (addaccess)
                                            {
                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    if ((currentPeer.Data as PlayerInfo).netID == Convert.ToInt32(netidss))
                                                    {

                                                        if ((currentPeer.Data as PlayerInfo).rawName == (peer.Data as PlayerInfo).rawName)
                                                        {
                                                            GamePacket p = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                             "`oYou cant access your self!``"));
                                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                            return;
                                                        }
                                                        string haveaccess = world.access;
                                                        foreach (string line in haveaccess.Split(",".ToCharArray()))
                                                        {
                                                            string[] ex = explode("|", line);
                                                            string idk = ex[0];

                                                            if (idk.IndexOf((currentPeer.Data as PlayerInfo).rawName) != -1)
                                                            {
                                                                GamePacket p = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`oThis player already have access!``"));
                                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                return;
                                                            }
                                                        }

                                                        if (world.access == "")
                                                        {
                                                            world.access = (currentPeer.Data as PlayerInfo).rawName + "|,";
                                                            GamePacket p = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`2You got the world access!``"));
                                                            currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        else
                                                        {
                                                            world.access = world.access + (currentPeer.Data as PlayerInfo).rawName + "|,";
                                                            GamePacket p = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`2You got the world access!``"));
                                                            currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                        }

                                                    }
                                                }
                                            }

                                            if (isSignDialog)
                                            {
                                                if ((peer.Data as PlayerInfo).currentWorld != "EXIT")
                                                {
                                                    int x = (peer.Data as PlayerInfo).lastPunchX;
                                                    int y = (peer.Data as PlayerInfo).lastPunchY;
                                                    if (signContent.Length < 128)
                                                    {
                                                        world.items[x + (y * world.width)].sign = signContent;
                                                        world.items[x + (y * world.width)].usedsign = 1;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int bg = world.items[x + (y * world.width)].background;
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (isHere(peer, currentPeer))
                                                            {
                                                                updatesign(currentPeer, fg, bg, x, y, signContent);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (isDoorDialog)
                                            {
                                                if ((peer.Data as PlayerInfo).currentWorld != "EXIT")
                                                {
                                                    int x = (peer.Data as PlayerInfo).lastPunchX;
                                                    int y = (peer.Data as PlayerInfo).lastPunchY;
                                                    if (dtext != "")
                                                    {
                                                        if (dtext.Length < 128)
                                                        {
                                                            world.items[x + (y * world.width)].dtext = dtext;
                                                            world.items[x + (y * world.width)].useddoor = 1;
                                                            world.items[x + (y * world.width)].dest = dest;
                                                            world.items[x + (y * world.width)].did = did;
                                                            int fg = world.items[x + (y * world.width)].foreground;
                                                            int bg = world.items[x + (y * world.width)].background;
                                                            foreach (ENetPeer currentPeer in peers)
                                                            {
                                                                if (currentPeer.State != ENetPeerState.Connected)
                                                                    continue;
                                                                if (isHere(peer, currentPeer))
                                                                {
                                                                    if (world.items[x + (y * world.width)].iop == 1)
                                                                    {
                                                                        updatedoor(currentPeer, fg, x, y, dtext);
                                                                    }
                                                                    else
                                                                    {
                                                                        string haveaccess1 = world.access;
                                                                        string access1 = "";
                                                                        foreach (string line in haveaccess1.Split(",".ToCharArray()))
                                                                        {
                                                                            string[] ex = explode("|", line);
                                                                            string idk = ex[0];

                                                                            access1 += idk;
                                                                        }
                                                                        if ((currentPeer.Data as PlayerInfo).rawName == world.owner || (currentPeer.Data as PlayerInfo).rawName == access1 || (currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                                        {
                                                                            updatedoor(currentPeer, fg, x, y, dtext);
                                                                        }
                                                                        else
                                                                        {
                                                                            lockdoor(currentPeer, fg, x, y, dtext);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        world.items[x + (y * world.width)].dtext = "...";
                                                        world.items[x + (y * world.width)].useddoor = 1;
                                                        int fg = world.items[x + (y * world.width)].foreground;
                                                        int bg = world.items[x + (y * world.width)].background;
                                                        world.items[x + (y * world.width)].dest = dest;
                                                        world.items[x + (y * world.width)].did = did;
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (isHere(peer, currentPeer))
                                                            {
                                                                if (world.items[x + (y * world.width)].iop == 1)
                                                                {
                                                                    updatedoor(currentPeer, fg, x, y, "...");
                                                                }
                                                                else
                                                                {
                                                                    string haveaccess1 = world.access;
                                                                    string access1 = "";
                                                                    foreach (string line in haveaccess1.Split(",".ToCharArray()))
                                                                    {
                                                                        string[] ex = explode("|", line);
                                                                        string idk = ex[0];

                                                                        access1 += idk;
                                                                    }
                                                                    if ((currentPeer.Data as PlayerInfo).rawName == world.owner || (currentPeer.Data as PlayerInfo).rawName == access1 || (currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                                    {
                                                                        updatedoor(currentPeer, fg, x, y, "...");
                                                                    }
                                                                    else
                                                                    {
                                                                        lockdoor(currentPeer, fg, x, y, "...");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (btn.Contains("emptyvend"))
                                            {
                                                int x = (peer.Data as PlayerInfo).lastPunchX;
                                                int y = (peer.Data as PlayerInfo).lastPunchY;
                                                int fg = world.items[x + (y * world.width)].foreground;
                                                int bg = world.items[x + (y * world.width)].background;

                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    if (isHere(peer, currentPeer))
                                                    {
                                                        updatevendtext(currentPeer, fg, x, y, "`2Vending Machine\n`wOUT OF ORDER");
                                                        world.items[x + (y * world.width)].price = 0;
                                                        world.items[x + (y * world.width)].sold = false;
                                                        world.items[x + (y * world.width)].invend = 0;
                                                        updatevend(currentPeer, x, y, 0, false, 0);
                                                    }
                                                }
                                            }

                                            if (isFindDialog && btn.Contains("tool"))
                                            {
                                                string id = btn.Split(new string[] { "tool" }, StringSplitOptions.None).Last();


                                                PlayerInventory inventory = new PlayerInventory();
                                                InventoryItem it = new InventoryItem();
                                                it.itemID = Convert.ToInt16(id);
                                                it.itemCount = 200;
                                                inventory.items = (peer.Data as PlayerInfo).inventory.items.Append(it).ToArray();

                                                (peer.Data as PlayerInfo).inventory = inventory;
                                                sendInventory(peer, (peer.Data as PlayerInfo).inventory);
                                                sendtradeaff(peer, Convert.ToInt16(id), (peer.Data as PlayerInfo).netID, (peer.Data as PlayerInfo).netID, 150);
                                            }
                                            else if (isFindDialog)
                                            {
                                                if (itemFind != "")
                                                {
                                                    string idk1 = itemFind.ToLower();
                                                    string listMiddle = "";
                                                    string listFull = "";
                                                    if (idk1.Length >= 3)
                                                    {
                                                        string contents = File.ReadAllText("CoreData.txt");
                                                        foreach (string line in contents.Split("\n".ToCharArray()))
                                                        {
                                                            if (line.Length > 8 && line[0] != '/' && line[1] != '/')
                                                            {
                                                                string[] ex = explode("|", line);
                                                                ItemDefinition def = new ItemDefinition();
                                                                def.id = Convert.ToInt32(ex[0]);
                                                                def.name = ex[1];
                                                                def.name.ToLower();
                                                                string idk = def.name.ToLower();
                                                                if (idk.IndexOf(idk1) != -1)
                                                                {

                                                                    listMiddle += "add_button_with_icon|tool" + def.id + "|`$" + def.name + "``|left|" + def.id + "||\n";
                                                                }
                                                            }
                                                        }
                                                        if (listMiddle == "")
                                                        {
                                                            showWrong(peer, listFull, itemFind);
                                                        }
                                                        else
                                                        {
                                                            GamePacket fff = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wFound item : " + itemFind + "``|left|6016|\nadd_spacer|small|\nadd_textbox|Enter a word below to find the item|\nadd_text_input|item|Item Name||20|\nend_dialog|findid|Cancel|Find the item!|\nadd_spacer|big|\n" + listMiddle + "add_quick_exit|\n"));
                                                            peer.Send(fff.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        listFull = "add_textbox|`4Word is less than 3 letters!``|\nadd_spacer|small|\n";
                                                        showWrong(peer, listFull, itemFind);
                                                    }
                                                }
                                            }

                                            if (changenamedialog)
                                            {
                                                GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rsoon``"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                            }

                                            if (isRegisterDialog)
                                            {

                                                int regState = PlayerDB.playerRegister(username, password, passwordverify, email,
                                                    discord);
                                                if (regState == 1)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rYour account has been created!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                                    GamePacket p2 = packetEnd(appendString(
                                                        appendString(appendInt(appendString(createPacket(), "SetHasGrowID"), 1),
                                                            username), password));
                                                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                    //enet_host_flush(server);
                                                    peer.DisconnectLater(0);
                                                }
                                                else if (regState == -1)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rAccount creation has failed, because it already exists!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -2)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rAccount creation has failed, because the name is too short!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -3)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`4Passwords mismatch!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -4)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`4Account creation has failed, because email address is invalid!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -5)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`4Account creation has failed, because Discord ID is invalid!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -7)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`5please remove the bad characters from the growid!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if (regState == -8)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`5Sorry but that name is used by the system please choose onther one!``"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|dialog_return");
                                        }
                                    }


                                    if (cch.IndexOf("action|trade_started") == 0)
                                    {
                                        string idk = cch.Split(new string[] { "netid|" }, StringSplitOptions.None).Last();
                                        int netid = Convert.ToInt32(idk);
                                        foreach (ENetPeer currentPeer in peers)
                                        {
                                            if (currentPeer.State != ENetPeerState.Connected)
                                                continue;
                                            if (!(peer.Data as PlayerInfo).radio)
                                                continue;
                                                tradestatus(peer, (currentPeer.Data as PlayerInfo).netID, "", (currentPeer.Data as PlayerInfo).displayName, "locked|0\nreset_locks|1\naccepted|0");
                                                tradestatus(currentPeer, (peer.Data as PlayerInfo).netID, "", (peer.Data as PlayerInfo).displayName, "locked|0\nreset_locks|1\naccepted|0");
                                        }
                                    }
                                    if (cch.IndexOf("action|trade_accept") == 0)
                                    {
                                        foreach (ENetPeer currentPeer in peers)
                                        {
                                            if (currentPeer.State != ENetPeerState.Connected)
                                                continue;
                                            if (!(peer.Data as PlayerInfo).radio)
                                                continue;
                                            if ((currentPeer.Data as PlayerInfo).rawName == (peer.Data as PlayerInfo).tradingme)
                                            {
                                                if ((currentPeer.Data as PlayerInfo).accepted == true)
                                                {
                                                    GamePacket p2 = packetEnd(appendString(createPacket(), "OnForceTradeEnd"));
                                                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                    currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                    //peer dialog
                                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wTrade Confirmation``|left|1366|\nadd_spacer|small|\n\nadd_label|small|`4You'll give:``|left|4|\nadd_spacer|small|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item1count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item1].name + "``|left|" + (peer.Data as PlayerInfo).item1 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item2count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item2].name + "``|left|" + (peer.Data as PlayerInfo).item2 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item3count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item3].name + "``|left|" + (peer.Data as PlayerInfo).item3 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item4count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item4].name + "``|left|" + (peer.Data as PlayerInfo).item4 + "|\nadd_spacer|small|\n\nadd_label|small|`2You'll get:``|left|4|\nadd_spacer|small|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item1count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item1].name + "``|left|" + (currentPeer.Data as PlayerInfo).item1 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item2count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item2].name + "``|left|" + (currentPeer.Data as PlayerInfo).item2 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item3count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item3].name + "``|left|" + (currentPeer.Data as PlayerInfo).item3 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item4count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item4].name + "``|left|" + (currentPeer.Data as PlayerInfo).item4 + "|\nadd_spacer|small|\nadd_button|dothetrade|`oDo The Trade!``|0|0|\nadd_button|notrade|`oCancel``|0|0|"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    //currentpeer dialog
                                                    GamePacket p1 = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wTrade Confirmation``|left|1366|\nadd_spacer|small|\n\nadd_label|small|`4You'll give:``|left|4|\nadd_spacer|small|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item1count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item1].name + "``|left|" + (currentPeer.Data as PlayerInfo).item1 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item2count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item2].name + "``|left|" + (currentPeer.Data as PlayerInfo).item2 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item3count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item3].name + "``|left|" + (currentPeer.Data as PlayerInfo).item3 + "|\n\nadd_label_with_icon|small|`o(`w" + (currentPeer.Data as PlayerInfo).item4count + "`o) " + itemDefs[(currentPeer.Data as PlayerInfo).item4].name + "``|left|" + (currentPeer.Data as PlayerInfo).item4 + "|\nadd_spacer|small|\n\nadd_label|small|`2You'll get:``|left|4|\nadd_spacer|small|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item1count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item1].name + "``|left|" + (peer.Data as PlayerInfo).item1 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item2count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item2].name + "``|left|" + (peer.Data as PlayerInfo).item2 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item3count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item3].name + "``|left|" + (peer.Data as PlayerInfo).item3 + "|\n\nadd_label_with_icon|small|`o(`w" + (peer.Data as PlayerInfo).item4count + "`o) " + itemDefs[(peer.Data as PlayerInfo).item4].name + "``|left|" + (peer.Data as PlayerInfo).item4 + "|\nadd_spacer|small|\nadd_button|dothetrade|`oDo The Trade!``|0|0|\nadd_button|notrade|`oCancel``|0|0|"));
                                                    currentPeer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else
                                                {
                                                    tradestatus(currentPeer, (peer.Data as PlayerInfo).netID, "", (peer.Data as PlayerInfo).displayName, "add_slot|" + (peer.Data as PlayerInfo).item1 + "|" + (peer.Data as PlayerInfo).item1count + "\nadd_slot|" + (peer.Data as PlayerInfo).item2 + "|" + (peer.Data as PlayerInfo).item2count + "\nadd_slot|" + (peer.Data as PlayerInfo).item3 + "|" + (peer.Data as PlayerInfo).item3count + "\nadd_slot|" + (peer.Data as PlayerInfo).item4 + "|" + (peer.Data as PlayerInfo).item4count + "locked|0\nreset_locks|0\naccepted|1");
                                                    (peer.Data as PlayerInfo).accepted = true;
                                                }
                                            }
                                        }
                                    }
                                    string tradeText = "action|mod_trade";
                                    if (cch.IndexOf(tradeText) == 0)
                                    {
                                        string idk = cch.Split(new string[] { "itemID|" }, StringSplitOptions.None).Last();
                                        int idd = Convert.ToInt32(idk);
                                        if (idd == 1) return;
                                        if ((peer.Data as PlayerInfo).item1 == 0)
                                        {
                                            if (idd == 18 || idd == 32)
                                            {
                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wYou'd be sorry sorry if you lost that!"));
                                                peer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                            else
                                            {
                                                (peer.Data as PlayerInfo).item1 = idd;
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`2Trade`w " + itemDefs[idd].name + "``|left|" + idd + "|\nadd_textbox|`2Trade how many?|\nadd_text_input|itemcount|||3|\nend_dialog|tradedialog|Cancel|Ok|\n"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                            }
                                            return;
                                        }


                                        if ((peer.Data as PlayerInfo).item2 == 0)
                                        {
                                            if (idd == 18 || idd == 32)
                                            {
                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wYou'd be sorry sorry if you lost that!"));
                                                peer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                            else
                                            {
                                                (peer.Data as PlayerInfo).item2 = idd;
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`2Trade`w " + itemDefs[idd].name + "``|left|" + idd + "|\nadd_textbox|`2Trade how many?|\nadd_text_input|itemcount2|||3|\nend_dialog|tradedialog|Cancel|Ok|\n"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                        }

                                        if ((peer.Data as PlayerInfo).item3 == 0)
                                        {
                                            if (idd == 18 || idd == 32)
                                            {
                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wYou'd be sorry sorry if you lost that!"));
                                                peer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                            else
                                            {
                                                (peer.Data as PlayerInfo).item3 = idd;
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`2Trade`w " + itemDefs[idd].name + "``|left|" + idd + "|\nadd_textbox|`2Trade how many?|\nadd_text_input|itemcount3|||3|\nend_dialog|tradedialog|Cancel|Ok|\n"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                        }

                                        if ((peer.Data as PlayerInfo).item4 == 0)
                                        {
                                            if (idd == 18 || idd == 32)
                                            {
                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wYou'd be sorry sorry if you lost that!"));
                                                peer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                            else
                                            {
                                                (peer.Data as PlayerInfo).item4 = idd;
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`2Trade`w " + itemDefs[idd].name + "``|left|" + idd + "|\nadd_textbox|`2Trade how many?|\nadd_text_input|itemcount4|||3|\nend_dialog|tradedialog|Cancel|Ok|\n"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                return;
                                            }
                                        }
                                    }
                                    if (cch.IndexOf("action|trade_cancel") == 0)
                                    {
                                        GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wYou canceled the trade."));
                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                        (peer.Data as PlayerInfo).item1 = 0;
                                        (peer.Data as PlayerInfo).item1count = 0;
                                        (peer.Data as PlayerInfo).item2 = 0;
                                        (peer.Data as PlayerInfo).item2count = 0;
                                        (peer.Data as PlayerInfo).item3 = 0;
                                        (peer.Data as PlayerInfo).item3count = 0;
                                        (peer.Data as PlayerInfo).item4 = 0;
                                        (peer.Data as PlayerInfo).item4count = 0;
                                        (peer.Data as PlayerInfo).tradingme = "";
                                        (peer.Data as PlayerInfo).istrading = false;
                                        (peer.Data as PlayerInfo).accepted = false;
                                        (peer.Data as PlayerInfo).dotrade = false;
                                        foreach (ENetPeer currentPeer in peers)
                                        {
                                            if (currentPeer.State != ENetPeerState.Connected)
                                                continue;
                                            if (!(peer.Data as PlayerInfo).radio)
                                                continue;
                                            if ((currentPeer.Data as PlayerInfo).rawName == (peer.Data as PlayerInfo).tradingme)
                                            {
                                                GamePacket p24 = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), (peer.Data as PlayerInfo).displayName + " `wcanceled the trade."));
                                                currentPeer.Send(p24.data, 0, ENetPacketFlags.Reliable);

                                                GamePacket p2 = packetEnd(appendString(createPacket(), "OnForceTradeEnd"));
                                                currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                (currentPeer.Data as PlayerInfo).item1 = 0;
                                                (currentPeer.Data as PlayerInfo).item1count = 0;
                                                (currentPeer.Data as PlayerInfo).item2 = 0;
                                                (currentPeer.Data as PlayerInfo).item2count = 0;
                                                (currentPeer.Data as PlayerInfo).item3 = 0;
                                                (currentPeer.Data as PlayerInfo).item3count = 0;
                                                (currentPeer.Data as PlayerInfo).item4 = 0;
                                                (currentPeer.Data as PlayerInfo).item4count = 0;
                                                (currentPeer.Data as PlayerInfo).tradingme = "";
                                                (currentPeer.Data as PlayerInfo).istrading = false;
                                                (currentPeer.Data as PlayerInfo).accepted = false;
                                                (currentPeer.Data as PlayerInfo).dotrade = false;
                                            }
                                        }
                                    }
                                    if (cch.IndexOf("action|rem_trade") == 0)
                                    {
                                        Console.WriteLine(cch);
                                    }

                                    string dropText = "action|drop\n|itemID|";
                                    if (cch.IndexOf(dropText) == 0)
                                    {
                                 /*       try
                                        {
                                            string idk = cch.Split(new string[] { "|itemID|" }, StringSplitOptions.None).Last();
                                            int idd = Convert.ToInt32(idk);
                                            if (idd == 1) return;
                                            (peer.Data as PlayerInfo).dropid = idd;
                                            if ((peer.Data as PlayerInfo).dropid == 18 || (peer.Data as PlayerInfo).dropid == 32)
                                            {
                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "You can't drop that."));
                                                peer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                            }
                                            else
                                            {
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wDrop " + itemDefs[(peer.Data as PlayerInfo).dropid].name + "``|left|" + (peer.Data as PlayerInfo).dropid + "|\nadd_textbox|`oHow many to drop?|\nadd_text_input|amount|||3|\nend_dialog|dropdialog|Cancel|Ok|\n"));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                            }
                                        }
                                        catch
                                        {

                                        }*/
                                        try
                                        {
                                            GamePacket p = packetEnd(appendString(
    appendString(createPacket(), "OnConsoleMessage"),
    "`rDrop is disabled for now!"));
                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("droptext");
                                        }
                                    }

                                    if (cch.Contains("text|"))
                                    {
                                        try
                                        {
                                            string str = cch.Split(new string[] { "text|" }, StringSplitOptions.None).Last();
                                            if (str == "/mod")
                                            {
                                                (peer.Data as PlayerInfo).canWalkInBlocks = true;
                                                sendState(peer);
                                            }
                                            else if (str == "/clearinv")
                                            {
                                                PlayerInventory inventory = new PlayerInventory();

                                                InventoryItem it = new InventoryItem();
                                                it.itemCount = 1;
                                                it.itemID = 18;
                                                inventory.items = inventory.items.Append(it).ToArray();
                                                it.itemID = 32;
                                                inventory.items = inventory.items.Append(it).ToArray();
                                                sendInventory(peer, inventory);

                                                (peer.Data as PlayerInfo).inventory = inventory;
                                            }
                                            else if (str == "/test")
                                            {

                                            }
                                            else if (str == "/nuke")
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    if (world.isNuked == false)
                                                    {
                                                        world.isNuked = true;
                                                        GamePacket p0 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`4You have nuked the world!"));
                                                        peer.Send(p0.data, 0, ENetPacketFlags.Reliable);
                                                        GamePacket p2 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`o>>`4" + world.name + " `4was nuked from orbit`o. It's the only way to be sure. Play nice, everybody!"));
                                                        string text = "action|play_sfx\nfile|audio/bigboom.wav\ndelayMS|0\n";
                                                        byte[] data = new byte[5 + text.Length];
                                                        int zero = 0;
                                                        int type = 3;
                                                        Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                        Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                        Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);

                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (!(peer.Data as PlayerInfo).radio)
                                                                continue;

                                                            currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                            currentPeer.Send(data, 0, ENetPacketFlags.Reliable);

                                                            if (isHere(peer, currentPeer))
                                                            {
                                                                if ((currentPeer.Data as PlayerInfo).adminLevel < 666)
                                                                {
                                                                    sendPlayerLeave(currentPeer, (currentPeer.Data as PlayerInfo));
                                                                    sendWorldOffers(currentPeer);
                                                                }
                                                            }

                                                            if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 nuked world: " + world.name + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        world.isNuked = false;
                                                        GamePacket p0 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`2You have un-nuke the world!"));
                                                        peer.Send(p0.data, 0, ENetPacketFlags.Reliable);

                                                    }
                                                }
                                            }
                                            else if (str == "/find")
                                            {
                                                if ((peer.Data as PlayerInfo).haveGrowId == false)
                                                {
                                                    GamePacket p0 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), " `4 you must `obe `2registered `oin order to use this command!"));
                                                    peer.Send(p0.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else
                                                {
                                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "add_label_with_icon|big|`wFind item``|left|6016|\nadd_textbox|Enter a word below to find the item|\nadd_text_input|item|Item Name||30|\nend_dialog|findid|Cancel|Find the item!|\nadd_quick_exit|\n"));
                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                            }
                                            else if (str.Length >= 7 && cch.Contains("/item "))
                                            {
                                                int netID = (peer.Data as PlayerInfo).netID;
                                                string itemos = cch.Split(new string[] { "item " }, StringSplitOptions.None).Last();
                                                if (itemos.Contains("-")) return;
                                                if (itemos.Length > 5) return;
                                                int Number;
                                                bool isNumber;
                                                isNumber = Int32.TryParse(itemos, out Number);

                                                if (!isNumber)
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    PlayerInventory inventory = new PlayerInventory();
                                                    InventoryItem it = new InventoryItem();
                                                    it.itemID = Convert.ToInt16(itemos);
                                                    it.itemCount = 200;
                                                    inventory.items = (peer.Data as PlayerInfo).inventory.items.Append(it).ToArray();

                                                    (peer.Data as PlayerInfo).inventory = inventory;
                                                    sendInventory(peer, (peer.Data as PlayerInfo).inventory);
                                                    sendtradeaff(peer, Convert.ToInt16(itemos), netID, netID, 150);
                                                }
                                            }
                                            else if (str.Length >= 7 && cch.Contains("/kick "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666 || (peer.Data as PlayerInfo).rawName == world.owner)
                                                {
                                                    string kickname = cch.Split(new string[] { "kick " }, StringSplitOptions.None).Last();
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).rawName == kickname)
                                                        {
                                                            respawn(currentPeer, false);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (str.Length >= 11 && cch.Contains("/unaccess "))
                                            {
                                                if ((peer.Data as PlayerInfo).rawName == world.owner || (peer.Data as PlayerInfo).adminLevel > 666)
                                                {
                                                    string name = cch.Split(new string[] { "access " }, StringSplitOptions.None).Last();
                                                    string hadi = name + "|,";
                                                    if (world.access.Contains(hadi))
                                                    {
                                                        world.access = world.access.Replace(hadi, "");
                                                        GamePacket p4 = packetEnd(appendString(
                                                       appendString(createPacket(), "OnConsoleMessage"),
                                                       "`5You have successfly removed " + name + " from access!"));
                                                        peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (!(peer.Data as PlayerInfo).radio)
                                                                continue;
                                                            if ((currentPeer.Data as PlayerInfo).rawName == name)
                                                            {
                                                                GamePacket pa = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                "`5You lost your access from world`$(`2" + world.name + "`&)!"));
                                                                currentPeer.Send(pa.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GamePacket p4 = packetEnd(appendString(
                                                          appendString(createPacket(), "OnConsoleMessage"),
                                                         "`4There is no one with `$(`2" + name + "`$) `5name have access in this world!"));
                                                        peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                }
                                            }
                                            else if (str.Length >= 10 && cch.Contains("/giveown "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    string newown = cch.Split(new string[] { "own " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave `5added `2 " + newown + " `6to Server Creator Team."));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;

                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == newown)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).adminLevel = 999;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 9 && cch.Contains("/demote "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    string newown = cch.Split(new string[] { "demote " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave `5demoted `4 " + newown));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;

                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == newown)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).adminLevel = 0;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 10 && cch.Contains("/givemod "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    string newmod = cch.Split(new string[] { "mod " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave `5added `2 " + newmod + " `#to Moderator Team."));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;

                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == newmod)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).adminLevel = 666;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 10 && cch.Contains("/givevip "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    string newvip = cch.Split(new string[] { "vip " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave `5added `2 " + newvip + " `rto VIP Team."));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;

                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == newvip)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).adminLevel = 333;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 6 && cch.Contains("/ban "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string banname = cch.Split(new string[] { "ban " }, StringSplitOptions.None).Last();
                                                    if (banname == "cmd" || banname == "hadi" || banname == "secret") return;
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave used `#Ban `oon `2" + banname + "`o! `#**"));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used ban on `4" + banname + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == banname)
                                                        {
                                                            GamePacket ps2 = packetEnd(appendInt(appendString(appendString(appendString(appendString(createPacket(), "OnAddNotification"),
                                                            "interface/atomic_button.rttex"), "`0Warning from `4System`0: You've been `4BANNED `0from Private Server for 730 days"), "audio/hub_open.wav"), 0));
                                                            currentPeer.Send(ps2.data, 0, ENetPacketFlags.Reliable);
                                                            (currentPeer.Data as PlayerInfo).isBanned = 1;
                                                            updatedb(currentPeer);
                                                            currentPeer.DisconnectLater(0);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 8 && cch.Contains("/curse "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string banname = cch.Split(new string[] { "curse " }, StringSplitOptions.None).Last();
                                                    if (banname == "cmd" || banname == "hadi" || banname == "secret") return;
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave used `bCurse `oon `2" + banname + "`o! `#**"));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used curse on `4" + banname + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == banname)
                                                        {
                                                            GamePacket ps2 = packetEnd(appendInt(appendString(appendString(appendString(appendString(createPacket(), "OnAddNotification"),
                                                            "interface/atomic_button.rttex"), "`0Warning from `4System`0: You've been `bCURSED `0from Private Server for 730 days"), "audio/hub_open.wav"), 0));
                                                            currentPeer.Send(ps2.data, 0, ENetPacketFlags.Reliable);
                                                            (currentPeer.Data as PlayerInfo).canleave = 1;
                                                            updatedb(currentPeer);
                                                            joinworld(currentPeer, "HELL", 0, 0);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 8 && cch.Contains("/unban "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string growid = cch.Split(new string[] { "unban " }, StringSplitOptions.None).Last();
                                                    string query = $"SELECT * FROM playerdb WHERE growid='{growid}';";
                                                    MySqlCommand hadi = new MySqlCommand(query, dbConn);
                                                    MySqlDataReader reader = hadi.ExecuteReader();
                                                    if (reader.Read())
                                                    {
                                                        string ib1 = (reader["isBanned"].ToString());
                                                        int cban = Convert.ToInt32(ib1);
                                                        string ai = (reader["accountID"].ToString());
                                                        string passo = (reader["password"].ToString());
                                                        string eemail = (reader["email"].ToString());
                                                        string disc = (reader["discord"].ToString());
                                                        string ad1 = (reader["adminLevel"].ToString());
                                                        string cb1 = (reader["ClothBack"].ToString());
                                                        string ch1 = (reader["ClothHand"].ToString());
                                                        string cf1 = (reader["ClothFace"].ToString());
                                                        string cs1 = (reader["ClothShirt"].ToString());
                                                        string cp1 = (reader["ClothPants"].ToString());
                                                        string cn1 = (reader["ClothNeck"].ToString());
                                                        string cha1 = (reader["ClothHair"].ToString());
                                                        string cfe1 = (reader["ClothFeet"].ToString());
                                                        string cm1 = (reader["ClothMask"].ToString());
                                                        string ca1 = (reader["ClothAnces"].ToString());
                                                        string a11 = (reader["allow1"].ToString());
                                                        string a21 = (reader["allow2"].ToString());
                                                        string a31 = (reader["allow3"].ToString());
                                                        string a41 = (reader["allow4"].ToString());
                                                        string a51 = (reader["allow5"].ToString());
                                                        string a61 = (reader["allow6"].ToString());
                                                        string a71 = (reader["allow7"].ToString());
                                                        string ll1 = (reader["Level"].ToString());
                                                        string cl1 = (reader["canleave"].ToString());
                                                        string im1 = (reader["isMuted"].ToString());
                                                        string ep1 = (reader["exp"].ToString());
                                                        string nt = "";
                                                        string gm1 = (reader["gem"].ToString());
                                                        if (cban == 1)
                                                        {
                                                            int accountID = Convert.ToInt32(ai);

                                                            int ad = Convert.ToInt32(ad1);
                                                            int cb = Convert.ToInt32(cb1);
                                                            int ch = Convert.ToInt32(ch1);
                                                            int cf = Convert.ToInt32(cf1);
                                                            int cs = Convert.ToInt32(cs1);
                                                            int cp = Convert.ToInt32(cp1);
                                                            int cn = Convert.ToInt32(cn1);
                                                            int cha = Convert.ToInt32(cha1);
                                                            int cfe = Convert.ToInt32(cfe1);
                                                            int cm = Convert.ToInt32(cm1);
                                                            int ca = Convert.ToInt32(ca1);
                                                            int a1 = Convert.ToInt32(a11);
                                                            int a2 = Convert.ToInt32(a21);
                                                            int a3 = Convert.ToInt32(a31);
                                                            int a4 = Convert.ToInt32(a41);
                                                            int a5 = Convert.ToInt32(a51);
                                                            int a6 = Convert.ToInt32(a61);
                                                            int a7 = Convert.ToInt32(a71);
                                                            int ll = Convert.ToInt32(ll1);
                                                            int cl = Convert.ToInt32(cl1);
                                                            int im = Convert.ToInt32(im1);
                                                            int ep = Convert.ToInt32(ep1);
                                                            int gm = Convert.ToInt32(gm1);
                                                            int ib = 0;
                                                            MySqlCommand cmd = new MySqlCommand("newacc", dbConn);
                                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                                            cmd.Parameters.AddWithValue("_accountID", accountID);
                                                            cmd.Parameters.AddWithValue("_growid", growid);
                                                            cmd.Parameters.AddWithValue("_password", passo);
                                                            cmd.Parameters.AddWithValue("_email", eemail);
                                                            cmd.Parameters.AddWithValue("_discord", disc);
                                                            cmd.Parameters.AddWithValue("_adminLevel", ad);
                                                            cmd.Parameters.AddWithValue("_ClothBack", cb);
                                                            cmd.Parameters.AddWithValue("_ClothHand", ch);
                                                            cmd.Parameters.AddWithValue("_ClothFace", cf);
                                                            cmd.Parameters.AddWithValue("_ClothShirt", cs);
                                                            cmd.Parameters.AddWithValue("_ClothPants", cp);
                                                            cmd.Parameters.AddWithValue("_ClothNeck", cn);
                                                            cmd.Parameters.AddWithValue("_ClothHair", cha);
                                                            cmd.Parameters.AddWithValue("_ClothFeet", cfe);
                                                            cmd.Parameters.AddWithValue("_ClothMask", cm);
                                                            cmd.Parameters.AddWithValue("_ClothAnces", ca);
                                                            cmd.Parameters.AddWithValue("_allow1", a1);
                                                            cmd.Parameters.AddWithValue("_allow2", a2);
                                                            cmd.Parameters.AddWithValue("_allow3", a3);
                                                            cmd.Parameters.AddWithValue("_allow4", a4);
                                                            cmd.Parameters.AddWithValue("_allow5", a5);
                                                            cmd.Parameters.AddWithValue("_allow6", a6);
                                                            cmd.Parameters.AddWithValue("_allow7", a7);
                                                            cmd.Parameters.AddWithValue("_Level", ll);
                                                            cmd.Parameters.AddWithValue("_canleave", cl);
                                                            cmd.Parameters.AddWithValue("_isMuted", im);
                                                            cmd.Parameters.AddWithValue("_exp", ep);
                                                            cmd.Parameters.AddWithValue("_isBanned", ib);
                                                            cmd.Parameters.AddWithValue("_friends", nt);
                                                            cmd.Parameters.AddWithValue("_gem", gm);
                                                            reader.Close();
                                                            cmd.ExecuteNonQuery();
                                                            GamePacket p2 = packetEnd(appendString(
                                                              appendString(createPacket(), "OnConsoleMessage"),
                                                              "`#** `$The Ancient Ones `ohave used `#Unban `oon `2" + growid + "`o! `#**"));
                                                            foreach (ENetPeer currentPeer in peers)
                                                            {
                                                                if (currentPeer.State != ENetPeerState.Connected)
                                                                    continue;
                                                                if (!(peer.Data as PlayerInfo).radio)
                                                                    continue;
                                                                if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                                {
                                                                    GamePacket p4 = packetEnd(appendString(
                                                                    appendString(createPacket(), "OnConsoleMessage"),
                                                                    "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used unban on `4" + growid + "!"));
                                                                    currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                                }

                                                                currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            reader.Close();
                                                            GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`5<" + growid + "> is not banned!"));
                                                            peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        reader.Close();
                                                        GamePacket p2 = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`5Didnt find the player! are you sure this is the correct name ?"));
                                                        peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 10 && cch.Contains("/uncurse "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string banname = cch.Split(new string[] { "uncurse " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave used `#Uncurse `oon `2" + banname + "`o! `#**"));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used uncurse on `4" + banname + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == banname)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).canleave = 0;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 9 && cch.Contains("/unmute "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string banname = cch.Split(new string[] { "unmute " }, StringSplitOptions.None).Last();
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave used `#Unmute `oon `2" + banname + "`o! `#**"));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used unmute on `4" + banname + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == banname)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).isMuted = 0;
                                                            (currentPeer.Data as PlayerInfo).cloth_face = 0;
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 7 && cch.Contains("/warn "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string warnname = cch.Split(new string[] { "warn " }, StringSplitOptions.None).Last();
                                                    string[] ex = explode(" ", warnname);
                                                    if (warnname.Contains(' '))
                                                    {
                                                        string warnname1 = ex[0];
                                                        string reason = cch.Split(new string[] { warnname1 + " " }, StringSplitOptions.None).Last();
                                                        if (warnname1 == "") return;
                                                        if (reason == "") return;
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (!(peer.Data as PlayerInfo).radio)
                                                                continue;
                                                            if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 warned `4" + warnname1 + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }

                                                            if ((currentPeer.Data as PlayerInfo).rawName == warnname1)
                                                            {
                                                                GamePacket ps2 = packetEnd(appendInt(appendString(appendString(appendString(appendString(createPacket(), "OnAddNotification"),
                                                                "interface/atomic_button.rttex"), "`wWarning from `4Admin`0: " + reason), "audio /hub_open.wav"), 0));
                                                                currentPeer.Send(ps2.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GamePacket p4 = packetEnd(appendString(
                                                               appendString(createPacket(), "OnConsoleMessage"),
                                                               "Please use it in this fromat /warn <player> <reason>"));
                                                        peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 6 && cch.Contains("/pay "))
                                            {
                                                string payname = cch.Split(new string[] { "pay " }, StringSplitOptions.None).Last();
                                                string[] ex = explode(" ", payname);
                                                if (payname.Contains(' '))
                                                {
                                                    string name = ex[0];
                                                    string amount = cch.Split(new string[] { name + " " }, StringSplitOptions.None).Last();
                                                    if (name == "") return;
                                                    if (amount == "") return;
                                                    if (amount.Contains("-")) return;
                                                    if (amount.Length > 5) return;
                                                    if (name == (peer.Data as PlayerInfo).rawName) return;
                                                    int Number;
                                                    bool isNumber;
                                                    isNumber = Int32.TryParse(amount, out Number);

                                                    if (!isNumber)
                                                    {
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        int gem = Convert.ToInt32(amount);
                                                        foreach (ENetPeer currentPeer in peers)
                                                        {
                                                            if (currentPeer.State != ENetPeerState.Connected)
                                                                continue;
                                                            if (!(peer.Data as PlayerInfo).radio)
                                                                continue;
                                                            if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Player `2" + (peer.Data as PlayerInfo).rawName + "`5 paid `4" + name + "`2" + amount + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }

                                                            if ((currentPeer.Data as PlayerInfo).rawName == name)
                                                            {
                                                                if ((peer.Data as PlayerInfo).gem < gem)
                                                                {
                                                                    GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`rSorry but you dont have enough gems!"));
                                                                    peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                                }
                                                                else
                                                                {
                                                                    (peer.Data as PlayerInfo).gem = (peer.Data as PlayerInfo).gem - gem;
                                                                    (currentPeer.Data as PlayerInfo).gem = (currentPeer.Data as PlayerInfo).gem + gem;
                                                                    GamePacket p2 = packetEnd(appendInt(appendString(createPacket(), "OnSetBux"), (peer.Data as PlayerInfo).gem));
                                                                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p3 = packetEnd(appendInt(appendString(createPacket(), "OnSetBux"), (currentPeer.Data as PlayerInfo).gem));
                                                                    currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    GamePacket p4 = packetEnd(appendString(
                                                     appendString(createPacket(), "OnConsoleMessage"),
                                                     "Please use it in this fromat /pay <player> <amount>"));
                                                    peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                }
                                            }

                                            else if (str.Length >= 6 && cch.Contains("/msg "))
                                            {
                                                string secret = cch.Split(new string[] { "msg " }, StringSplitOptions.None).Last();
                                                string[] ex = explode(" ", secret);
                                                if (secret.Contains(' '))
                                                {
                                                    sendconsolemsg(peer, "CP:0_PL:4_OID:_CT:[MSG]_ `6" + str);
                                                    string msgname = ex[0];
                                                    string msg = cch.Split(new string[] { msgname + " " }, StringSplitOptions.None).Last();
                                                    if (msgname == "") return;
                                                    if (msg == "") return;
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            if ((peer.Data as PlayerInfo).adminLevel == 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + msgname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                  appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`r[MOD LOGS] `6The Developer `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + msgname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else if ((peer.Data as PlayerInfo).adminLevel == 333)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                  "`r[MOD LOGS] `rThe VIP `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + msgname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`r[MOD LOGS] `oThe Player `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + msgname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }

                                                        if ((currentPeer.Data as PlayerInfo).rawName == msgname)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).lastmsger = (peer.Data as PlayerInfo).rawName;
                                                            (currentPeer.Data as PlayerInfo).lastmsgerworld = (peer.Data as PlayerInfo).currentWorld;
                                                            if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "CP:0_PL:4_OID:_CT:[MSG]_ `c>> from (`2" + (peer.Data as PlayerInfo).displayName + "`c) in [`$" + (peer.Data as PlayerInfo).currentWorld + "`c] > `$" + msg + "`o"));
                                                                currentPeer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`6(Sent to `$" + msgname + "`6) `$(`4Note: `omsg a mod `4ONLY ONCE `oabout issue. mods don't fix scams or replace items. they punish the players who break the `5/rules`o. For issues related to account please contact `5server creator on discord.`$)"));
                                                                peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                                string text = "action|play_sfx\nfile|audio/pay_time.wav\ndelayMS|0\n";
                                                                byte[] data = new byte[5 + text.Length];
                                                                int zero = 0;
                                                                int type = 3;
                                                                Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                                Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);
                                                                currentPeer.Send(data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else
                                                            {
                                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "CP:0_PL:4_OID:_CT:[MSG]_ `c>> from (`2" + (peer.Data as PlayerInfo).displayName + "`c) in [`$" + (peer.Data as PlayerInfo).currentWorld + "`c] > `$" + msg + "`o"));
                                                                currentPeer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`6(Sent to `$" + msgname + "`6)"));
                                                                peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                                string text = "action|play_sfx\nfile|audio/pay_time.wav\ndelayMS|0\n";
                                                                byte[] data = new byte[5 + text.Length];
                                                                int zero = 0;
                                                                int type = 3;
                                                                Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                                Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);
                                                                currentPeer.Send(data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    GamePacket p4 = packetEnd(appendString(
                                                             appendString(createPacket(), "OnConsoleMessage"),
                                                             "Please use it in this fromat /msg <player> <reason>"));
                                                    peer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                }
                                            }
                                            else if (str.Length >= 4 && cch.Contains("/r "))
                                            {
                                                string msg = cch.Split(new string[] { "r " }, StringSplitOptions.None).Last();
                                                if ((peer.Data as PlayerInfo).lastmsger == "")
                                                {
                                                    GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnTextOverlay"), "`wno one sent you a message"));
                                                    peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else
                                                {
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            if ((peer.Data as PlayerInfo).adminLevel == 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + (peer.Data as PlayerInfo).lastmsger + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                  appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`r[MOD LOGS] `6The Developer `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + (peer.Data as PlayerInfo).lastmsger + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else if ((peer.Data as PlayerInfo).adminLevel == 333)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                  "`r[MOD LOGS] `rThe VIP `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + (peer.Data as PlayerInfo).lastmsger + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                 appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`r[MOD LOGS] `oThe Player `2" + (peer.Data as PlayerInfo).rawName + "`5 sent a message to `4" + (peer.Data as PlayerInfo).lastmsger + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }

                                                        if ((currentPeer.Data as PlayerInfo).rawName == (peer.Data as PlayerInfo).lastmsger)
                                                        {
                                                            (currentPeer.Data as PlayerInfo).lastmsger = (peer.Data as PlayerInfo).rawName;
                                                            (currentPeer.Data as PlayerInfo).lastmsgerworld = (peer.Data as PlayerInfo).currentWorld;
                                                            if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "CP:0_PL:4_OID:_CT:[MSG]_ `c>> from (`2" + (peer.Data as PlayerInfo).displayName + "`c) in [`$" + (peer.Data as PlayerInfo).currentWorld + "`c] > `$" + msg + "`o"));
                                                                currentPeer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`6(Sent to `$" + (peer.Data as PlayerInfo).lastmsger + "`6) `$(`4Note: `omsg a mod `4ONLY ONCE `oabout issue. mods don't fix scams or replace items. they punish the players who break the `5/rules`o. For issues related to account please contact `5server creator on discord.`$)"));
                                                                peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                                string text = "action|play_sfx\nfile|audio/pay_time.wav\ndelayMS|0\n";
                                                                byte[] data = new byte[5 + text.Length];
                                                                int zero = 0;
                                                                int type = 3;
                                                                Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                                Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);
                                                                currentPeer.Send(data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else
                                                            {
                                                                GamePacket ps = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "CP:0_PL:4_OID:_CT:[MSG]_ `c>> from (`2" + (peer.Data as PlayerInfo).displayName + "`c) in [`$" + (peer.Data as PlayerInfo).currentWorld + "`c] > `$" + msg + "`o"));
                                                                currentPeer.Send(ps.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`6(Sent to `$" + (peer.Data as PlayerInfo).lastmsger + "`6)"));
                                                                peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                                string text = "action|play_sfx\nfile|audio/pay_time.wav\ndelayMS|0\n";
                                                                byte[] data = new byte[5 + text.Length];
                                                                int zero = 0;
                                                                int type = 3;
                                                                Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                                Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);
                                                                currentPeer.Send(data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (str.Length >= 9 && cch.Contains("/summon "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string summonname = cch.Split(new string[] { "summon " }, StringSplitOptions.None).Last();
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            if ((peer.Data as PlayerInfo).adminLevel == 666)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                appendString(createPacket(), "OnConsoleMessage"),
                                                                "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 summoned `4" + summonname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                            else if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                            {
                                                                GamePacket p4 = packetEnd(appendString(
                                                                  appendString(createPacket(), "OnConsoleMessage"),
                                                                 "`r[MOD LOGS] `6The Developer `2" + (peer.Data as PlayerInfo).rawName + "`5 summoned `4" + summonname + "!"));
                                                                currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                        if ((currentPeer.Data as PlayerInfo).rawName == summonname)
                                                        {
                                                            sendPlayerLeave(currentPeer, (currentPeer.Data as PlayerInfo));
                                                            int x = (peer.Data as PlayerInfo).x;
                                                            int y = (peer.Data as PlayerInfo).y;
                                                            string act = (peer.Data as PlayerInfo).currentWorld;
                                                            joinworld(currentPeer, act, x, y);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 7 && cch.Contains("/mute "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string mutename = cch.Split(new string[] { "mute " }, StringSplitOptions.None).Last();
                                                    if (mutename == "cmd" || mutename == "hadi" || mutename == "secret") return;
                                                    GamePacket p2 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`#** `$The Ancient Ones `ohave used `#Mute `oon `2" + mutename + "`o! `#**"));
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `#The Moderator `2" + (peer.Data as PlayerInfo).rawName + "`5 used mute on `4" + mutename + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                        currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);

                                                        if ((currentPeer.Data as PlayerInfo).rawName == mutename)
                                                        {
                                                            GamePacket ps2 = packetEnd(appendInt(appendString(appendString(appendString(appendString(createPacket(), "OnAddNotification"),
                                                            "interface/atomic_button.rttex"), "`0Warning from `4System`0: You've been `4MUTED `0from Private Server for 730 days"), "audio/hub_open.wav"), 0));
                                                            currentPeer.Send(ps2.data, 0, ENetPacketFlags.Reliable);
                                                            (currentPeer.Data as PlayerInfo).isMuted = 1;
                                                            (currentPeer.Data as PlayerInfo).cloth_face = 408;
                                                            sendClothes(currentPeer);
                                                            updatedb(currentPeer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 10 && cch.Contains("/copyset "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 333)
                                                {
                                                    string name = cch.Split(new string[] { "set " }, StringSplitOptions.None).Last();
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;

                                                        if ((currentPeer.Data as PlayerInfo).rawName == name)
                                                        {
                                                            (peer.Data as PlayerInfo).cloth_back = (currentPeer.Data as PlayerInfo).cloth_back;
                                                            (peer.Data as PlayerInfo).cloth_face = (currentPeer.Data as PlayerInfo).cloth_face;
                                                            (peer.Data as PlayerInfo).cloth_ances = (currentPeer.Data as PlayerInfo).cloth_ances;
                                                            (peer.Data as PlayerInfo).cloth_hair = (currentPeer.Data as PlayerInfo).cloth_hair;
                                                            (peer.Data as PlayerInfo).cloth_hand = (currentPeer.Data as PlayerInfo).cloth_hand;
                                                            (peer.Data as PlayerInfo).cloth_mask = (currentPeer.Data as PlayerInfo).cloth_mask;
                                                            (peer.Data as PlayerInfo).cloth_neck = (currentPeer.Data as PlayerInfo).cloth_neck;
                                                            (peer.Data as PlayerInfo).cloth_pants = (currentPeer.Data as PlayerInfo).cloth_pants;
                                                            (peer.Data as PlayerInfo).cloth_shirt = (currentPeer.Data as PlayerInfo).cloth_shirt;
                                                            sendClothes(peer);
                                                            updatedb(peer);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str == "/rgo")
                                            {
                                                if ((peer.Data as PlayerInfo).lastmsger == "")
                                                {
                                                    GamePacket ps1 = packetEnd(appendString(appendString(createPacket(), "OnTextOverLay"), "`wno one sent u a message"));
                                                    peer.Send(ps1.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else
                                                {
                                                    sendPlayerLeave(peer, (peer.Data as PlayerInfo));
                                                    string act = (peer.Data as PlayerInfo).lastmsgerworld;
                                                    joinworld(peer, act, 0, 0);
                                                }
                                            }


                                            else if (str == "/help")
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`6Server Creator commands are: /giveown <player> , /givemod <player> , /givevip <player> , /ban <player>, /mute <player>, /warn <player> <reason>, /nuke, /nick <name>, /summon <name>, /demote <name>, /warp <world>, /copyset <name>, /warpto <name>, /curse <name>, /unban <name>, /unmute <name>, /uncurse <name> " + "\n\n" + "`oSupported commands are: /help, /mod, /unmod, /item id, /who, /count, /sb message, /alt, /radio, /msg <player> <msg> /r <msg>, /rgo, /find, /pay <player> <amount>"));

                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if ((peer.Data as PlayerInfo).adminLevel == 666)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`#Moderator commands are: /ban <player>, /mute <player>, /warn <player> <reason>, /nuke, /nick <name>, /summon <name>, /warp <world>, /copyset <name>, /warpto <name> /curse <name>, /unban <name>, /unmute <name>, /uncurse <name> " + "\n\n" + "`oSupported commands are: /help, /mod, /unmod, /item id, /who, /count, /sb message, /alt, /radio, /msg <player> <msg> /r <msg>, /rgo, /find, /pay <player> <amount>"));

                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else if ((peer.Data as PlayerInfo).adminLevel == 333)
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rVIP commands are: /warp <world>, /copyset <name>, /warpto <name>, /invis " + "\n\n" + "Supported commands are: /help, /mod, /unmod, /item id, /who, /count, /sb message, /alt, /radio, /msg <player> <msg> /r <msg>, /rgo, /find, /pay <player> <amount>"));

                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                }
                                                else
                                                {
                                                    GamePacket p = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "Supported commands are: /help, /mod, /unmod, /item id, /who, /count, /sb message, /radio, /msg <player> <msg> /r <msg>, /rgo, /find, /pay <player> <amount>"));

                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    //enet_host_flush(server);
                                                }
                                            }
                                            else if (str == "/invis")
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 333)
                                                {
                                                    if ((peer.Data as PlayerInfo).isinv == false)
                                                    {
                                                        sendconsolemsg(peer, "`oNo one can see you. `$(Invisible Mod Added!)");
                                                        GamePacket p2 = packetEnd(appendFloat(appendString(createPacket(), "OnSetPos"), (peer.Data as PlayerInfo).x, (peer.Data as PlayerInfo).y));
                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                        peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                        sendState(peer);
                                                        sendClothes(peer);
                                                        (peer.Data as PlayerInfo).isinv = true;
                                                        GamePacket p0 = packetEnd(appendInt(appendString(createPacket(), "OnInvis"), 1));
                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p0.data, 8, 4);
                                                        peer.Send(p0.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                    else
                                                    {
                                                        sendconsolemsg(peer, "`oYou lost your power everyone can see you. `$(Invisible Mod removed)");
                                                        GamePacket p2 = packetEnd(appendFloat(appendString(createPacket(), "OnSetPos"), (peer.Data as PlayerInfo).x1, (peer.Data as PlayerInfo).y1));
                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                        peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                        sendState(peer);
                                                        sendClothes(peer);
                                                        (peer.Data as PlayerInfo).isinv = false;
                                                        GamePacket p0 = packetEnd(appendInt(appendString(createPacket(), "OnInvis"), 0));
                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p0.data, 8, 4);
                                                        peer.Send(p0.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 7 && cch.Contains("/nick "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 666)
                                                {
                                                    string newname = cch.Split(new string[] { "nick " }, StringSplitOptions.None).Last();
                                                    (peer.Data as PlayerInfo).displayName = newname;
                                                    GamePacket p3 = packetEnd(appendString(appendString(createPacket(), "OnNameChanged"), newname + "`````"));
                                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (isHere(peer, currentPeer))
                                                        {
                                                            currentPeer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 10 && cch.Contains("/weather "))
                                            {
                                                string aa = cch.Split(new string[] { "weather " }, StringSplitOptions.None).Last();
                                                int a = Convert.ToInt32(aa);
                                                GamePacket p = packetEnd(appendInt(
                                                appendString(createPacket(), "OnSetCurrentWeather"), a));
                                                world.weather = a;
                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    if (isHere(peer, currentPeer))
                                                    {
                                                        currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 3 && cch.Contains("/p "))
                                            {
                                                string eff = cch.Split(new string[] { "p " }, StringSplitOptions.None).Last();
                                                int Number;
                                                bool isNumber;
                                                isNumber = Int32.TryParse(eff, out Number);

                                                if (!isNumber)
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    (peer.Data as PlayerInfo).effect = Convert.ToInt32(eff);
                                                    sendState(peer); //here
                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                }
                                            }

                                            else if (str.Length >= 7 && cch.Contains("/warp "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 333)
                                                {
                                                    string tworld = cch.Split(new string[] { "warp " }, StringSplitOptions.None).Last();
                                                    string name = tworld;
                                                    if (name == "CON" || name == "PRN" || name == "AUX" || name == "NUL" || name == "COM1" || name == "COM2" || name == "COM3" || name == "COM4" || name == "COM5" || name == "COM6" || name == "COM7" || name == "COM8" || name == "COM9" || name == "LPT1" || name == "LPT2" || name == "LPT3" || name == "LPT4" || name == "LPT5" || name == "LPT6" || name == "LPT7" || name == "LPT8" || name == "LPT9" || name == "con" || name == "prn" || name == "aux" || name == "nul" || name == "com1" || name == "com2" || name == "com3" || name == "com4" || name == "com5" || name == "com6" || name == "com7" || name == "com8" || name == "com9" || name == "lpt1" || name == "lpt2" || name == "lpt3" || name == "lpt4" || name == "lpt5" || name == "lpt6" || name == "lpt7" || name == "lpt8" || name == "lpt9")
                                                    {
                                                        (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                        GamePacket pzo = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`4Sorry `w this world is used by the system"));
                                                        peer.Send(pzo.data, 0, ENetPacketFlags.Reliable);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (tworld == "exit" || tworld == "EXIT")
                                                        {
                                                            sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                            (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                            sendWorldOffers(peer);
                                                            updatedb(peer);
                                                        }
                                                        else
                                                        {
                                                            sendPlayerLeave(peer, (peer.Data as PlayerInfo));
                                                            joinworld(peer, tworld, 0, 0);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 9 && cch.Contains("/warpto "))
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel >= 333)
                                                {
                                                    string pname = cch.Split(new string[] { "warpto " }, StringSplitOptions.None).Last();
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (!(peer.Data as PlayerInfo).radio)
                                                            continue;
                                                        if ((currentPeer.Data as PlayerInfo).adminLevel >= 666)
                                                        {
                                                            GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnConsoleMessage"),
                                                            "`r[MOD LOGS] `2" + (peer.Data as PlayerInfo).rawName + "`5 warp on `4" + pname + "!"));
                                                            currentPeer.Send(p4.data, 0, ENetPacketFlags.Reliable);
                                                        }

                                                        if ((currentPeer.Data as PlayerInfo).rawName == pname)
                                                        {
                                                            sendPlayerLeave(peer, (peer.Data as PlayerInfo));
                                                            int x = (currentPeer.Data as PlayerInfo).x;
                                                            int y = (currentPeer.Data as PlayerInfo).y;
                                                            string pworld = (currentPeer.Data as PlayerInfo).currentWorld;
                                                            joinworld(peer, pworld, x, y);
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str.Length >= 8 && cch.Contains("/trade "))
                                            {
                                                string name = cch.Split(new string[] { "trade " }, StringSplitOptions.None).Last();
                                                if ((peer.Data as PlayerInfo).rawName == name)
                                                {
                                                    sendconsolemsg(peer, "You cant trade your self!");
                                                    return;
                                                }
                                                else if ((peer.Data as PlayerInfo).istrading == true)
                                                {
                                                    sendconsolemsg(peer, "cancel the current trade first");
                                                    return;
                                                }
                                                if ((peer.Data as PlayerInfo).tradingme == name)
                                                {
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (isHere(peer, currentPeer))
                                                        {
                                                            (peer.Data as PlayerInfo).istrading = true;
                                                            (currentPeer.Data as PlayerInfo).istrading = true;
                                                            GamePacket pt1 = packetEnd(appendInt(appendString(appendString(createPacket(), "OnStartTrade"), (currentPeer.Data as PlayerInfo).rawName), (currentPeer.Data as PlayerInfo).netID));
                                                            peer.Send(pt1.data, 0, ENetPacketFlags.Reliable);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        if (isHere(peer, currentPeer))
                                                        {
                                                            if ((currentPeer.Data as PlayerInfo).rawName == name)
                                                            {
                                                                if ((currentPeer.Data as PlayerInfo).istrading == true)
                                                                {
                                                                    sendconsolemsg(peer, "this player is trading someone else");
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    (peer.Data as PlayerInfo).tradingme = name;
                                                                    (currentPeer.Data as PlayerInfo).tradingme = (peer.Data as PlayerInfo).rawName;
                                                                    GamePacket p1 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "You started trading with " + (currentPeer.Data as PlayerInfo).rawName));
                                                                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"), "`#TRADE ALERT: `w" + (peer.Data as PlayerInfo).rawName + " `owants to trade with you! To start, use the `wWrench `oon that person's wrench icon,or type `w/trade " + (peer.Data as PlayerInfo).rawName));
                                                                    currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket pt1 = packetEnd(appendInt(appendString(appendString(createPacket(), "OnStartTrade"), (currentPeer.Data as PlayerInfo).rawName), (currentPeer.Data as PlayerInfo).netID));
                                                                    peer.Send(pt1.data, 0, ENetPacketFlags.Reliable);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            else if (str == "/count")
                                            {
                                                int count = 0;

                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    count++;
                                                }

                                                GamePacket p = packetEnd(appendString(
                                                    appendString(createPacket(), "OnConsoleMessage"),
                                                    "There are " + count + " people online out of 1024 limit."));

                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                //enet_host_flush(server);
                                            }


                                            else if (str == "/reset")
                                            {
                                                if ((peer.Data as PlayerInfo).adminLevel == 999)
                                                {
                                                    Console.WriteLine("Restart from " + (peer.Data as PlayerInfo).displayName);
                                                    GamePacket p = packetEnd(appendInt(
                                                        appendString(
                                                            appendString(
                                                                appendString(appendString(createPacket(), "OnAddNotification"),
                                                                    "interface/science_button.rttex"), "Restarting soon!"),
                                                            "audio/mp3/suspended.mp3"), 0));

                                                    foreach (ENetPeer currentPeer in peers)
                                                    {
                                                        if (currentPeer.State != ENetPeerState.Connected)
                                                            continue;
                                                        currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                    }

                                                    //enet_host_flush(server);
                                                }
                                            }
                                            else if (str == "/unmod")
                                            {
                                                (peer.Data as PlayerInfo).canWalkInBlocks = false;
                                                sendState(peer);
                                            }
                                            else if (str.Length >= 3 && cch.Contains("/sb "))
                                            {

                                                string sbmsg = cch.Split(new string[] { "sb " }, StringSplitOptions.None).Last();
                                                long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                                if ((peer.Data as PlayerInfo).lastSB + 45000 < time)
                                                {
                                                    (peer.Data as PlayerInfo).lastSB = time;
                                                }
                                                else
                                                {
                                                    GamePacket p1 = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "Wait a minute before using the SB command again!"));

                                                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                                                    //enet_host_flush(server);
                                                    return;
                                                }
                                                string name = (peer.Data as PlayerInfo).displayName;
                                                GamePacket p2 = packetEnd(appendString(
                                                    appendString(createPacket(), "OnConsoleMessage"),
                                                    "`w** `5Super-Broadcast`` from `$`2" + name + "```` (in `$" +
                                                    (peer.Data as PlayerInfo).currentWorld + "``) ** :`` `# " +
                                                    sbmsg));

                                                string text = "action|play_sfx\nfile|audio/beep.wav\ndelayMS|0\n";
                                                byte[] data = new byte[5 + text.Length];
                                                int zero = 0;
                                                int type = 3;
                                                Array.Copy(BitConverter.GetBytes(type), 0, data, 0, 4);
                                                Array.Copy(Encoding.ASCII.GetBytes(text), 0, data, 4, text.Length);
                                                Array.Copy(BitConverter.GetBytes(zero), 0, data, 4 + text.Length, 1);

                                                foreach (ENetPeer currentPeer in peers)
                                                {
                                                    if (currentPeer.State != ENetPeerState.Connected)
                                                        continue;
                                                    if (!(peer.Data as PlayerInfo).radio)
                                                        continue;

                                                    currentPeer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                    currentPeer.Send(data, 0, ENetPacketFlags.Reliable);

                                                }
                                            }
                                            else if (str.Length != 0 && str[0] == '/')
                                            {
                                                sendAction(peer, (peer.Data as PlayerInfo).netID, str);
                                            }
                                            else if (str.Length > 0)
                                            {
                                                sendChatMessage(peer, (peer.Data as PlayerInfo).netID, str);
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|text");
                                        }
                                    }
                                    if (!(peer.Data as PlayerInfo).isIn)
                                    {
                                        try
                                        {
                                            uint a = 130113087;
                                            int hash = (int)a;
                                            GamePacket p = packetEnd(appendString(
                                                appendString(
                                                    appendString(
                                                        appendString(
                                                            appendInt(
                                                                appendString(createPacket(),
                                                            //														"OnSuperMainStartAcceptLogonHrdxs47254722215a"), -703607114),
                                                            "OnSuperMainStartAcceptLogonHrdxs47254722215a"), hash),
                                                            "ubistatic-a.akamaihd.net"), "0098/CDNContent48/cache/"),
                                                    "cc.cz.madkite.freedom org.aqua.gg idv.aqua.bulldog com.cih.gamecih2 com.cih.gamecih com.cih.game_cih cn.maocai.gamekiller com.gmd.speedtime org.dax.attack com.x0.strai.frep com.x0.strai.free org.cheatengine.cegui org.sbtools.gamehack com.skgames.traffikrider org.sbtoods.gamehaca com.skype.ralder org.cheatengine.cegui.xx.multi1458919170111 com.prohiro.macro me.autotouch.autotouch com.cygery.repetitouch.free com.cygery.repetitouch.pro com.proziro.zacro com.slash.gamebuster"),
                                                "proto=42|choosemusic=audio/mp3/about_theme.mp3|active_holiday=0|"));
                                            //for (int i = 0; i < p.len; i++) cout << (int)*(p.data + i) << " ";
                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                            //enet_host_flush(server);
                                            string[] str = Encoding.ASCII.GetString(pak.Take(pak.Length - 1).Skip(4).ToArray()).Split("\n".ToCharArray());
                                            foreach (string to in str)
                                            {
                                                Console.WriteLine(to);
                                                if (to == "") continue;
                                                string[] ex = explode("|", to);
                                                string id = ex[0];
                                                string act = ex[1];
                                                if (id == "tankIDName")
                                                {
                                                    (peer.Data as PlayerInfo).tankIDName = act;
                                                    (peer.Data as PlayerInfo).haveGrowId = true;
                                                }
                                                else if (id == "tankIDPass")
                                                {
                                                    (peer.Data as PlayerInfo).tankIDPass = act;
                                                }
                                                else if (id == "requestedName")
                                                {
                                                    (peer.Data as PlayerInfo).requestedName = act;
                                                }
                                                else if (id == "country")
                                                {
                                                    (peer.Data as PlayerInfo).country = act;
                                                }
                                                else if (id == "rid")
                                                {
                                                    if (act.Length < 30) peer.DisconnectLater(0);
                                                    if (act.Length > 38) peer.DisconnectLater(0);
                                                }
                                                else if (id == "zf")
                                                {
                                                    if (act.Length < 4) peer.DisconnectLater(0);
                                                    (peer.Data as PlayerInfo).zf = act;
                                                }
                                                else if (id == "game_verison")
                                                {
                                                    if (act.Length < 4) peer.DisconnectLater(0);
                                                    (peer.Data as PlayerInfo).gameverison = act;
                                                }
                                                else if (id == "platformID")
                                                {
                                                    if (act.Length == 0) peer.DisconnectLater(0);
                                                    (peer.Data as PlayerInfo).platformID = act;
                                                }
                                                else if (id == "wk")
                                                {
                                                   /* if ((peer.Data as PlayerInfo).platformID == "0")
                                                    {
                                                        if (act.Length < 30) peer.DisconnectLater(0);
                                                        if (act.Length > 36) peer.DisconnectLater(0);
                                                        (peer.Data as PlayerInfo).wk = act;
                                                    }*/
                                                }
                                                else if (id == "mac")
                                                {
                                                    if (act.Length < 15) return;
                                                    if (act.Length > 20) return;
                                                    (peer.Data as PlayerInfo).mac = act;
                                                }
                                                else if (id == "hash")
                                                {
                                                    if (act.Length != 0)
                                                    {
                                                        if (act.Length < 6) peer.DisconnectLater(0);
                                                        if (act.Length > 16) peer.DisconnectLater(0);
                                                    }
                                                }
                                                else if (id == "hash2")
                                                {
                                                    if (act.Length != 0)
                                                    {
                                                        if (act.Length < 6) peer.DisconnectLater(0);
                                                        if (act.Length > 16) peer.DisconnectLater(0);
                                                    }
                                                }
                                            }

                                            if (!(peer.Data as PlayerInfo).haveGrowId)
                                            {
                                                (peer.Data as PlayerInfo).rawName = "";
                                                (peer.Data as PlayerInfo).displayName = "Fake " + PlayerDB.fixColors((peer.Data as PlayerInfo).
                                                    requestedName.Substring(0, (peer.Data as PlayerInfo).
                                                    requestedName.Length > 15 ? 15 : (peer.Data as PlayerInfo).
                                                    requestedName.Length));
                                            }
                                            else
                                            {
                                                (peer.Data as PlayerInfo).rawName =
                                                    PlayerDB.getProperName((peer.Data as PlayerInfo).tankIDName);
                                                int logStatus = PlayerDB.playerLogin(peer, (peer.Data as PlayerInfo).rawName,
                                                    (peer.Data as PlayerInfo).tankIDPass);
                                                if (logStatus == 1)
                                                {
                                                    GamePacket p1 = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rYou have successfully logged into your account!``"));
                                                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                                                    (peer.Data as PlayerInfo).displayName = (peer.Data as PlayerInfo).tankIDName;
                                                }
                                                else
                                                {
                                                    GamePacket p1 = packetEnd(appendString(
                                                        appendString(createPacket(), "OnConsoleMessage"),
                                                        "`rWrong username or password!``"));

                                                    peer.Send(p1.data, 0, ENetPacketFlags.Reliable);
                                                    peer.DisconnectLater(0);
                                                    return;
                                                }
                                            }

                                            foreach (char c in (peer.Data as PlayerInfo).displayName)
                                                if (c < 0x20 || c > 0x7A) (peer.Data as PlayerInfo).displayName = "Bad characters in name, remove them!";

                                            if ((peer.Data as PlayerInfo).country.Length > 4)
                                            {
                                                (peer.Data as PlayerInfo).country = "us";
                                            }
                                            if (getAdminLevel((peer.Data as PlayerInfo).rawName, (peer.Data as PlayerInfo).tankIDPass) > 0)
                                            {
                                                (peer.Data as PlayerInfo).country = "../cash_icon_overlay";
                                            }

                                            GamePacket p2 = packetEnd(appendString(appendString(
                                                appendInt(appendString(createPacket(), "SetHasGrowID"),
                                                    ((peer.Data as PlayerInfo).haveGrowId ? 1 : 0))
                                                , (peer.Data as PlayerInfo).tankIDName)
                                                , (peer.Data as PlayerInfo).tankIDPass));

                                            peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in onsupermain thing");
                                        }
                                    }

                                    string pStr = Encoding.ASCII.GetString(pak.Take(pak.Length - 1).Skip(4).ToArray());
                                //if (strcmp(GetTextPointerFromPacket(event.packet), "action|enter_game\n") == 0 && !((PlayerInfo*)(event.peer->data))->isIn)
                                if (pStr.Contains("action|enter_game") && !(peer.Data as PlayerInfo).isIn)
                                    {
                                        try
                                        {
                                            long lastjoin = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                            if ((peer.Data as PlayerInfo).lastent + 4000 < lastjoin)
                                            {
                                                (peer.Data as PlayerInfo).lastent = lastjoin;
                                            }
                                            else
                                            {
                                                peer.DisconnectLater(0);
                                                return;
                                            }
                                            Console.WriteLine("And we are in!");
                                            (peer.Data as PlayerInfo).log = 1;
                                            (peer.Data as PlayerInfo).isIn = true;
                                            updatecloth(peer);
                                            sendClothes(peer);
                                            if ((peer.Data as PlayerInfo).isBanned == 1)
                                            {
                                                GamePacket p3 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
        "`5Sorry this account (`w" + (peer.Data as PlayerInfo).rawName + "`5) is suspended please contact Server Creators on DISCORD"));

                                                peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                peer.DisconnectLater(0);
                                                return;
                                            }
                                            if ((peer.Data as PlayerInfo).displayName.Contains("@") || (peer.Data as PlayerInfo).displayName.Contains("#") || (peer.Data as PlayerInfo).displayName.Contains("!") || (peer.Data as PlayerInfo).displayName.Contains("`") || (peer.Data as PlayerInfo).displayName.Contains("$") || (peer.Data as PlayerInfo).displayName.Contains("%") || (peer.Data as PlayerInfo).displayName.Contains("^")
                                             || (peer.Data as PlayerInfo).displayName.Contains("&") || (peer.Data as PlayerInfo).displayName.Contains("*") || (peer.Data as PlayerInfo).displayName.Contains("(") || (peer.Data as PlayerInfo).displayName.Contains(")") || (peer.Data as PlayerInfo).displayName.Contains("-") || (peer.Data as PlayerInfo).displayName.Contains("_") || (peer.Data as PlayerInfo).displayName.Contains("+")
                                              || (peer.Data as PlayerInfo).displayName.Contains("[") || (peer.Data as PlayerInfo).displayName.Contains("]") || (peer.Data as PlayerInfo).displayName.Contains("|") || (peer.Data as PlayerInfo).displayName.Contains("'") || (peer.Data as PlayerInfo).displayName.Contains(",") || (peer.Data as PlayerInfo).displayName.Contains(":") || (peer.Data as PlayerInfo).displayName.Contains(";")
                                               || (peer.Data as PlayerInfo).displayName.Contains(" ") || (peer.Data as PlayerInfo).displayName.Contains(".") || (peer.Data as PlayerInfo).displayName.Contains("="))
                                            {
                                                if ((peer.Data as PlayerInfo).haveGrowId == false)
                                                {

                                                }
                                                else
                                                {
                                                    GamePacket p3 = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
                                                     "`5Please remove the bad characters from your name first before you join!"));

                                                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                    peer.DisconnectLater(0);
                                                    return;
                                                }
                                            }
                                            if ((peer.Data as PlayerInfo).haveGrowId == true)
                                            {
                                                sendWorldOffers(peer);
                                                long xas = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                                (peer.Data as PlayerInfo).lastent1 = xas;
                                            }
                                            else
                                            {
                                                GamePacket pza = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"),
                                                 "set_default_color|`o\n\nadd_label_with_icon|big|`wGet a GrowID``|left|206|\n\nadd_spacer|small|\nadd_textbox|A `wGrowID `wmeans `oyou can use a name and password to logon from any device.|\nadd_spacer|small|\nadd_textbox|This `wname `owill be reserved for you and `wshown to other players`o, so choose carefully!|\nadd_text_input|username|GrowID||30|\nadd_text_input|password|Password||100|\nadd_text_input|passwordverify|Password Verify||100|\nadd_textbox|Your `wemail address `owill only be used for account verification purposes and won't be spammed or shared. If you use a fake email, you'll never be able to recover or change your password.|\nadd_text_input|email|Email||100|\nadd_textbox|Your `wDiscord ID `owill be used for secondary verification if you lost access to your `wemail address`o! Please enter in such format: `wdiscordname#tag`o. Your `wDiscord Tag `ocan be found in your `wDiscord account settings`o.|\nadd_text_input|discord|Discord||100|\nend_dialog|register|Cancel|Get My GrowID!|\n"));
                                                peer.Send(pza.data, 0, ENetPacketFlags.Reliable);
                                            }
                                            if ((peer.Data as PlayerInfo).adminLevel == 999)
                                            {
                                                if ((peer.Data as PlayerInfo).rawName == "hadi")
                                                {
                                                    (peer.Data as PlayerInfo).displayName = "`4@" + (peer.Data as PlayerInfo).displayName;
                                                }
                                                else
                                                {
                                                    (peer.Data as PlayerInfo).displayName = "`6@" + (peer.Data as PlayerInfo).displayName;
                                                }
                                            }
                                            else if ((peer.Data as PlayerInfo).adminLevel == 666)
                                            {
                                                (peer.Data as PlayerInfo).displayName = "`#@" + (peer.Data as PlayerInfo).displayName;
                                            }
                                            else if ((peer.Data as PlayerInfo).adminLevel == 333)
                                            {
                                                (peer.Data as PlayerInfo).displayName = "`r[VIP] " + (peer.Data as PlayerInfo).displayName;
                                            }
                                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
                                            "Welcome to Project Reborn V2"));

                                            peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                            //enet_host_flush(server);
                                            PlayerInventory inventory = new PlayerInventory();

                                            InventoryItem it = new InventoryItem();
                                            it.itemCount = 1;
                                            it.itemID = 18;
                                            inventory.items = inventory.items.Append(it).ToArray();
                                            it.itemID = 32;
                                            inventory.items = inventory.items.Append(it).ToArray();
                                            sendInventory(peer, inventory);

                                            (peer.Data as PlayerInfo).inventory = inventory;

                                            {
                                                //GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnDialogRequest"), "set_default_color|`o\n\nadd_label_with_icon|big|`wThe Growtopia Gazette``|left|5016|\n\nadd_spacer|small|\n\nadd_image_button|banner|interface/large/news_banner.rttex|noflags|||\n\nadd_spacer|small|\n\nadd_textbox|`wSeptember 10:`` `5Surgery Stars end!``|left|\n\nadd_spacer|small|\n\n\n\nadd_textbox|Hello Growtopians,|left|\n\nadd_spacer|small|\n\n\n\nadd_textbox|Surgery Stars is over! We hope you enjoyed it and claimed all your well-earned Summer Tokens!|left|\n\nadd_spacer|small|\n\nadd_spacer|small|\n\nadd_textbox|As we announced earlier, this month we are releasing the feature update a bit later, as we're working on something really cool for the monthly update and we're convinced that the wait will be worth it!|left|\n\nadd_spacer|small|\n\nadd_textbox|Check the Forum here for more information!|left|\n\nadd_spacer|small|\n\nadd_url_button|comment|`wSeptember Updates Delay``|noflags|https://www.growtopiagame.com/forums/showthread.php?510657-September-Update-Delay&p=3747656|Open September Update Delay Announcement?|0|0|\n\nadd_spacer|small|\n\nadd_spacer|small|\n\nadd_textbox|Also, we're glad to invite you to take part in our official Growtopia survey!|left|\n\nadd_spacer|small|\n\nadd_url_button|comment|`wTake Survey!``|noflags|https://ubisoft.ca1.qualtrics.com/jfe/form/SV_1UrCEhjMO7TKXpr?GID=26674|Open the browser to take the survey?|0|0|\n\nadd_spacer|small|\n\nadd_textbox|Click on the button above and complete the survey to contribute your opinion to the game and make Growtopia even better! Thanks in advance for taking the time, we're looking forward to reading your feedback!|left|\n\nadd_spacer|small|\n\nadd_spacer|small|\n\nadd_textbox|And for those who missed PAW, we made a special video sneak peek from the latest PAW fashion show, check it out on our official YouTube channel! Yay!|left|\n\nadd_spacer|small|\n\nadd_url_button|comment|`wPAW 2018 Fashion Show``|noflags|https://www.youtube.com/watch?v=5i0IcqwD3MI&feature=youtu.be|Open the Growtopia YouTube channel for videos and tutorials?|0|0|\n\nadd_spacer|small|\n\nadd_textbox|Lastly, check out other September updates:|left|\n\nadd_spacer|small|\n\nadd_label_with_icon|small|IOTM: The Sorcerer's Tunic of Mystery|left|24|\n\nadd_label_with_icon|small|New Legendary Summer Clash Branch|left|24|\n\nadd_spacer|small|\n\nadd_textbox|`$- The Growtopia Team``|left|\n\nadd_spacer|small|\n\nadd_spacer|small|\n\n\n\n\n\nadd_url_button|comment|`wOfficial YouTube Channel``|noflags|https://www.youtube.com/c/GrowtopiaOfficial|Open the Growtopia YouTube channel for videos and tutorials?|0|0|\n\nadd_url_button|comment|`wSeptember's IOTM: `8Sorcerer's Tunic of Mystery!````|noflags|https://www.growtopiagame.com/forums/showthread.php?450065-Item-of-the-Month&p=3392991&viewfull=1#post3392991|Open the Growtopia website to see item of the month info?|0|0|\n\nadd_spacer|small|\n\nadd_label_with_icon|small|`4WARNING:`` `5Drop games/trust tests`` and betting games (like `5Casinos``) are not allowed and will result in a ban!|left|24|\n\nadd_label_with_icon|small|`4WARNING:`` Using any kind of `5hacked client``, `5spamming/text pasting``, or `5bots`` (even with an alt) will likely result in losing `5ALL`` your accounts. Seriously.|left|24|\n\nadd_label_with_icon|small|`4WARNING:`` `5NEVER enter your GT password on a website (fake moderator apps, free gemz, etc) - it doesn't work and you'll lose all your stuff!|left|24|\n\nadd_spacer|small|\n\nadd_url_button|comment|`wGrowtopia on Facebook``|noflags|http://growtopiagame.com/facebook|Open the Growtopia Facebook page in your browser?|0|0|\n\nadd_spacer|small|\n\nadd_button|rules|`wHelp - Rules - Privacy Policy``|noflags|0|0|\n\n\nadd_quick_exit|\n\nadd_spacer|small|\nadd_url_button|comment|`wVisit Growtopia Forums``|noflags|http://www.growtopiagame.com/forums|Visit the Growtopia forums?|0|0|\nadd_spacer|small|\nadd_url_button||`wWOTD: `1THELOSTGOLD`` by `#iWasToD````|NOFLAGS|OPENWORLD|THELOSTGOLD|0|0|\nadd_spacer|small|\nadd_url_button||`wVOTW: `1Yodeling Kid - Growtopia Animation``|NOFLAGS|https://www.youtube.com/watch?v=UMoGmnFvc58|Watch 'Yodeling Kid - Growtopia Animation' by HyerS on YouTube?|0|0|\nend_dialog|gazette||OK|"));
                                                GamePacket p4 = packetEnd(appendString(
                                                            appendString(createPacket(), "OnDialogRequest"),
                                                              "set_default_color|`o\n\nadd_label_with_icon|big|`wProject reborn V2``|left|5016|\n\nadd_spacer|small|\nadd_label_with_icon|small|`2Server have been developed by `4HADI and CMD Hosted by Mickea.|left|4|\nadd_label_with_icon|small|`4Warning: Server is still in beta so its not include all future|left|4|\nadd_spacer|small|\n\nadd_url_button||``Watch: `1Watch a video about GT Private Server``|NOFLAGS|https://www.youtube.com/watch?v=_3avlDDYBBY|Open link?|0|0|\nadd_url_button||``Channel: `1Watch Growtopia Noobs' channel``|NOFLAGS|https://www.youtube.com/channel/UCLXtuoBlrXFDRtFU8vPy35g|Open link?|0|0|\nadd_url_button||``Items: `1Item database by Nenkai``|NOFLAGS|https://raw.githubusercontent.com/Nenkai/GrowtopiaItemDatabase/master/GrowtopiaItemDatabase/CoreData.txt|Open link?|0|0|\nadd_url_button||``Discord: `1GT Private Server Discord``|NOFLAGS|https://discord.gg/8WUTs4v|Open the link?|0|0|\nadd_quick_exit|\nadd_button|chc0|Close|noflags|0|0|\nnend_dialog|gazette||OK|"));
                                                peer.Send(p4.data, 0, ENetPacketFlags.Reliable);

                                                //enet_host_flush(server);
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in action|enter_game");
                                        }
                                    }
                                    if (Encoding.ASCII.GetString(pak.Take(pak.Length - 1).Skip(4).ToArray()) == "action|refresh_item_data\n")
                                    {

                                        try
                                        {
                                            if (itemsDat != null)
                                            {
                                                GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
                                  "`oOne moment, updating items data..."));
                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);

                                                peer.Send(itemsDat, 0, ENetPacketFlags.Reliable);
                                            //  peer.Send(FromHex("0400000016000000000000000000000000000000000000001500000000000000000000000000000000000000000000000000000000000000000000000c"), 0, ENetPacketFlags.Reliable);
                                            (peer.Data as PlayerInfo).isUpdating = true;
                                                string mo = BitConverter.ToString(itemsDat);
                                                mo = mo.Replace("-", "");
                                            //Console.WriteLine("Itemsdat: " + mo);
                                            peer.DisconnectLater(0);
                                            //enet_host_flush(server);
                                        }

                                        // TODO FIX refresh_item_data ^^^^^^^^^^^^^^
                                    }
                                        catch
                                        {
                                            Console.WriteLine("error in refresh_items_dat");
                                        }

                                    // TODO FIX refresh_item_data ^^^^^^^^^^^^^^
                                }
                                    break;
                                }
                            default:
                                Console.WriteLine("Unknown packet type " + messageType);
                                break;
                            case 3:
                                {
                                //cout << GetTextPointerFromPacket(event.packet) << endl;
                                bool isJoinReq = false;
                                    foreach (string to in Encoding.ASCII.GetString(pak.Take(pak.Length - 1).Skip(4).ToArray()).Split("\n".ToCharArray()))
                                    {
                                        try
                                        {
                                            if (to == "") continue;
                                            string id = to.Substring(0, to.IndexOf("|"));
                                            string act = to.Substring(to.IndexOf("|") + 1, to.Length - to.IndexOf("|") - 1);
                                            if (id == "name" && isJoinReq)
                                            {
                                                joinworld(peer, act, 0, 0);
                                            }

                                            if (id == "action")
                                            {

                                                if (act == "join_request")
                                                {
                                                    isJoinReq = true;
                                                }

                                                if (act == "quit_to_exit")
                                                {
                                                    sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                    sendWorldOffers(peer);
                                                    updatedb(peer);
                                                    long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                                    if ((peer.Data as PlayerInfo).cansave + 300000 < time)
                                                    {
                                                        (peer.Data as PlayerInfo).cansave = time;
                                                        worldDB.saveAll();
                                                    }
                                                    else
                                                    {

                                                    }

                                                }

                                                if (act == "quit")
                                                {
                                                    updatedb(peer);
                                                    worldDB.saveAll();
                                                    peer.DisconnectLater(0);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("error in quit to exit");
                                        }
                                    }

                                    break;
                                }
                            case 4:
                                {
                                    {
                                        byte[] tankUpdatePacket = pak.Skip(4).ToArray();
                                        if (tankUpdatePacket.Length != 0)
                                        {
                                            try
                                            {
                                                PlayerMoving pMov = unpackPlayerMoving(tankUpdatePacket);
                                                if ((peer.Data as PlayerInfo).isinv == true)
                                                {
                                                    (peer.Data as PlayerInfo).isInvisible = true;
                                                    (peer.Data as PlayerInfo).x1 = (int)pMov.x;
                                                    (peer.Data as PlayerInfo).y1 = (int)pMov.y;
                                                    pMov.x = -1000000;
                                                    pMov.y = -1000000;
                                                }
                                                switch (pMov.packetType)
                                                {
                                                    case 0:
                                                        (peer.Data as PlayerInfo).x = (int)pMov.x;
                                                        (peer.Data as PlayerInfo).y = (int)pMov.y;
                                                        (peer.Data as PlayerInfo).isRotatedLeft = (pMov.characterState & 0x10) != 0;
                                                        sendPData(peer, pMov);
                                                        if (!(peer.Data as PlayerInfo).joinClothesUpdated)
                                                        {
                                                            (peer.Data as PlayerInfo).joinClothesUpdated = true;
                                                            updateAllClothes(peer);
                                                        }

                                                        break;

                                                    default:
                                                        break;
                                                }

                                                PlayerMoving data2 = unpackPlayerMoving(tankUpdatePacket);
                                                //cout << data2->packetType << endl;
                                                if (data2.packetType == 11)
                                                {
                                                    sendtake(peer, (peer.Data as PlayerInfo).netID, (int)pMov.x, (int)pMov.y, data2.plantingTree);
                                                    Console.WriteLine(data2.plantingTree);

                                                }

                                                if (data2.packetType == 25)
                                                {
                                                    if ((peer.Data as PlayerInfo).haveGrowId == true)
                                                    {
                                                        if ((peer.Data as PlayerInfo).adminLevel >= 333)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            GamePacket p = packetEnd(appendString(appendString(createPacket(), "OnConsoleMessage"),
                                                            "`4** `$" + (peer.Data as PlayerInfo).rawName + " `4AUTO-BANNED BY SYSTEM **`o(`$/rules `oto view rules)"));

                                                            GamePacket ps2 = packetEnd(appendInt(appendString(appendString(appendString(appendString(createPacket(), "OnAddNotification"),
                                                            "interface/atomic_button.rttex"), "`wWarning from `4System`0: Cheat Engine! You account will be suspended bye!"), "audio /hub_open.wav"), 0));
                                                            peer.Send(ps2.data, 0, ENetPacketFlags.Reliable);
                                                            (peer.Data as PlayerInfo).isBanned = 1;
                                                            updatedb(peer);
                                                            peer.DisconnectLater(0);
                                                            foreach (ENetPeer currentPeer in peers)
                                                            {
                                                                if (currentPeer.State != ENetPeerState.Connected)
                                                                    continue;
                                                                if (!(peer.Data as PlayerInfo).radio)
                                                                    continue;

                                                                currentPeer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        peer.DisconnectLater(0);
                                                    }
                                                }

                                                if (data2.packetType == 7)
                                                {
                                                    if (pMov.punchX < 0 || pMov.punchY < 0 || pMov.punchX > 100 || pMov.punchY > 100) return;
                                                    if ((peer.Data as PlayerInfo).currentWorld == " EXIT") return;
                                                    int x = pMov.punchX;
                                                    int y = pMov.punchY;
                                                    int tile = world.items[x + (y * world.width)].foreground;
                                                    string dest = world.items[x + (y * world.width)].dest;
                                                    string did = world.items[x + (y * world.width)].did;
                                                    int iop = world.items[x + (y * world.width)].iop;
                                                    int netID = (peer.Data as PlayerInfo).netID;
                                                    if (tile == 6)
                                                    {
                                                        sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                        (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                        sendWorldOffers(peer);
                                                        updatedb(peer);
                                                    }
                                                    else if (tile == 410 || tile == 1832 || tile == 1770)
                                                    {
                                                        (peer.Data as PlayerInfo).cpX = x * 32;
                                                        (peer.Data as PlayerInfo).cpY = y * 32;
                                                        GamePacket p3 = packetEnd(appendInt(appendString(createPacket(), "SetRespawnPos"), x + (y * world.width)));
                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                        peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                    }
                                                    else if (tile == 12)
                                                    {
                                                        if (iop == 1)
                                                        {
                                                            string name = dest;
                                                            if (name == "CON" || name == "PRN" || name == "AUX" || name == "NUL" || name == "COM1" || name == "COM2" || name == "COM3" || name == "COM4" || name == "COM5" || name == "COM6" || name == "COM7" || name == "COM8" || name == "COM9" || name == "LPT1" || name == "LPT2" || name == "LPT3" || name == "LPT4" || name == "LPT5" || name == "LPT6" || name == "LPT7" || name == "LPT8" || name == "LPT9" || name == "con" || name == "prn" || name == "aux" || name == "nul" || name == "com1" || name == "com2" || name == "com3" || name == "com4" || name == "com5" || name == "com6" || name == "com7" || name == "com8" || name == "com9" || name == "lpt1" || name == "lpt2" || name == "lpt3" || name == "lpt4" || name == "lpt5" || name == "lpt6" || name == "lpt7" || name == "lpt8" || name == "lpt9" || name == "")
                                                            {
                                                                GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket p3 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                                peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                return;
                                                            }
                                                            if ((peer.Data as PlayerInfo).canleave == 1)
                                                            {
                                                                GamePacket p3 = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`wThe door laugh at you."));
                                                                peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket p2 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                                peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                                return;
                                                            }
                                                            if (dest == "exit" || dest == "EXIT")
                                                            {
                                                                sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                                sendWorldOffers(peer);
                                                                updatedb(peer);
                                                            }
                                                            if (dest.Contains(":"))
                                                            {
                                                                string[] ex = explode(":", dest);
                                                                if (ex[0] == "")
                                                                {
                                                                    WorldInfo info = getPlyersWorld(peer);
                                                                    int xo = 0;
                                                                    int yo = 0;

                                                                    for (int j = 0; j < info.width * info.height; j++)
                                                                    {
                                                                        if (info.items[j].did == ex[1])
                                                                        {
                                                                            xo = (j % info.width) * 32;
                                                                            yo = (j / info.width) * 32;
                                                                        }
                                                                    }
                                                                    GamePacket p2 = packetEnd(appendFloat(appendString(createPacket(), "OnSetPos"), xo, yo));
                                                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p3 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                }
                                                                else
                                                                {
                                                                    WorldInfo info = worldDB.get2(ex[0]).info;
                                                                    int xo = 0;
                                                                    int yo = 0;

                                                                    for (int j = 0; j < info.width * info.height; j++)
                                                                    {
                                                                        if (info.items[j].did == ex[1])
                                                                        {
                                                                            xo = (j % info.width) * 32;
                                                                            yo = (j / info.width) * 32;
                                                                        }
                                                                    }
                                                                    sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                    (peer.Data as PlayerInfo).currentWorld = ex[0];
                                                                    joinworld(peer, ex[0], xo, yo);
                                                                    updatedb(peer);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                (peer.Data as PlayerInfo).currentWorld = dest;
                                                                joinworld(peer, dest, 0, 0);
                                                                updatedb(peer);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string haveaccess1 = world.access;
                                                            string access1 = "";
                                                            foreach (string line in haveaccess1.Split(",".ToCharArray()))
                                                            {
                                                                string[] ex = explode("|", line);
                                                                string idk = ex[0];

                                                                access1 += idk;
                                                            }
                                                            if ((peer.Data as PlayerInfo).rawName == world.owner || (peer.Data as PlayerInfo).rawName == access1 || (peer.Data as PlayerInfo).adminLevel >= 666)
                                                            {
                                                                string name = dest;
                                                                if (name == "CON" || name == "PRN" || name == "AUX" || name == "NUL" || name == "COM1" || name == "COM2" || name == "COM3" || name == "COM4" || name == "COM5" || name == "COM6" || name == "COM7" || name == "COM8" || name == "COM9" || name == "LPT1" || name == "LPT2" || name == "LPT3" || name == "LPT4" || name == "LPT5" || name == "LPT6" || name == "LPT7" || name == "LPT8" || name == "LPT9" || name == "con" || name == "prn" || name == "aux" || name == "nul" || name == "com1" || name == "com2" || name == "com3" || name == "com4" || name == "com5" || name == "com6" || name == "com7" || name == "com8" || name == "com9" || name == "lpt1" || name == "lpt2" || name == "lpt3" || name == "lpt4" || name == "lpt5" || name == "lpt6" || name == "lpt7" || name == "lpt8" || name == "lpt9" || name == "")
                                                                {
                                                                    GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p3 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                    return;
                                                                }
                                                                if ((peer.Data as PlayerInfo).canleave == 1)
                                                                {
                                                                    GamePacket p3 = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`wThe door laugh at you."));
                                                                    peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                    peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                    GamePacket p2 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                    Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                                    peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                                    return;
                                                                }
                                                                if (dest == "exit" || dest == "EXIT")
                                                                {
                                                                    sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                    (peer.Data as PlayerInfo).currentWorld = "EXIT";
                                                                    sendWorldOffers(peer);
                                                                    updatedb(peer);
                                                                }
                                                                if (dest.Contains(":"))
                                                                {
                                                                    string[] ex = explode(":", dest);
                                                                    if (ex[0] == "")
                                                                    {
                                                                        WorldInfo info = getPlyersWorld(peer);
                                                                        int xo = 0;
                                                                        int yo = 0;

                                                                        for (int j = 0; j < info.width * info.height; j++)
                                                                        {
                                                                            if (info.items[j].did == ex[1])
                                                                            {
                                                                                xo = (j % info.width) * 32;
                                                                                yo = (j / info.width) * 32;
                                                                            }
                                                                        }
                                                                        GamePacket p2 = packetEnd(appendFloat(appendString(createPacket(), "OnSetPos"), xo, yo));
                                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                                        peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                                        GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                        peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                        GamePacket p3 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                        Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p3.data, 8, 4);
                                                                        peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                    }
                                                                    else
                                                                    {
                                                                        WorldInfo info = worldDB.get2(ex[0]).info;
                                                                        int xo = 0;
                                                                        int yo = 0;

                                                                        for (int j = 0; j < info.width * info.height; j++)
                                                                        {
                                                                            if (info.items[j].did == ex[1])
                                                                            {
                                                                                xo = (j % info.width) * 32;
                                                                                yo = (j / info.width) * 32;
                                                                            }
                                                                        }
                                                                        sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                        (peer.Data as PlayerInfo).currentWorld = ex[0];
                                                                        joinworld(peer, ex[0], xo, yo);
                                                                        updatedb(peer);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    sendPlayerLeave(peer, peer.Data as PlayerInfo);
                                                                    (peer.Data as PlayerInfo).currentWorld = dest;
                                                                    joinworld(peer, dest, 0, 0);
                                                                    updatedb(peer);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                GamePacket p3 = packetEnd(appendString(appendIntx(appendString(createPacket(), "OnTalkBubble"), (peer.Data as PlayerInfo).netID), "`wThe door is locked."));
                                                                peer.Send(p3.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket p = packetEnd(appendInt(appendString(createPacket(), "OnZoomCamera"), 2));
                                                                peer.Send(p.data, 0, ENetPacketFlags.Reliable);
                                                                GamePacket p2 = packetEnd(appendIntx(appendString(createPacket(), "OnSetFreezeState"), 0));
                                                                Array.Copy(BitConverter.GetBytes((peer.Data as PlayerInfo).netID), 0, p2.data, 8, 4);
                                                                peer.Send(p2.data, 0, ENetPacketFlags.Reliable);
                                                            }
                                                        }
                                                    }
                                                }

                                                if (data2.packetType == 10)
                                                {
                                                    //cout << pMov->x << ";" << pMov->y << ";" << pMov->plantingTree << ";" << pMov->punchX << ";" << pMov->punchY << ";" << pMov->characterState << endl;
                                                    ItemDefinition def = new ItemDefinition();
                                                    try
                                                    {
                                                        def = getItemDef(pMov.plantingTree);
                                                    }
                                                    catch
                                                    {

                                                    }

                                                    switch (def.clothType)
                                                    {
                                                        case ClothTypes.HAIR:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_hair == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_hair = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_hair = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.SHIRT:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_shirt == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_shirt = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_shirt = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.PANTS:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_pants == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_pants = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_pants = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.FEET:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_feet == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_feet = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_feet = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.FACE:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_face == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_face = 0;
                                                                    break;
                                                                }
                                                                if (pMov.plantingTree == 1204)
                                                                { // f eyes
                                                                    (peer.Data as PlayerInfo).effect = 10;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (pMov.plantingTree == 138 || pMov.plantingTree == 2976)
                                                                { // cyclo (laser)
                                                                    (peer.Data as PlayerInfo).effect = 1;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_face = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.HAND:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_hand == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_hand = 0;
                                                                    break;
                                                                }
                                                                int item = pMov.plantingTree;
                                                                if (item == 1782)
                                                                {  //legendary dragon
                                                                    (peer.Data as PlayerInfo).effect = 8421397;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 366)
                                                                { // heart bow
                                                                    (peer.Data as PlayerInfo).effect = 2;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2952)
                                                                { // diger spade
                                                                    (peer.Data as PlayerInfo).effect = 8421405;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 472)
                                                                { // tommygun
                                                                    (peer.Data as PlayerInfo).effect = 3;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 594)
                                                                { // Elvish Longbow
                                                                    (peer.Data as PlayerInfo).effect = 4;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 768)
                                                                { // shotgun
                                                                    (peer.Data as PlayerInfo).effect = 5;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 910 || item == 1250)
                                                                { // frank
                                                                    (peer.Data as PlayerInfo).effect = 7;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1378 || item == 2866)
                                                                { // frank
                                                                    (peer.Data as PlayerInfo).effect = 11;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1512)
                                                                { // shotgun
                                                                    (peer.Data as PlayerInfo).effect = 14;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1548)
                                                                { // battle trout
                                                                    (peer.Data as PlayerInfo).effect = 15;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1576)
                                                                { // fiesta dragon
                                                                    (peer.Data as PlayerInfo).effect = 16;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1676)
                                                                { // squirt gun
                                                                    (peer.Data as PlayerInfo).effect = 17;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1874)
                                                                { // ring of force
                                                                    (peer.Data as PlayerInfo).effect = 24;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1946)
                                                                { // ice calf 
                                                                    (peer.Data as PlayerInfo).effect = 25;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1974)
                                                                { // night mare
                                                                    (peer.Data as PlayerInfo).effect = 26;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 1956)
                                                                { // cursed 
                                                                    (peer.Data as PlayerInfo).effect = 27;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2212)
                                                                { // black crystal dragin
                                                                    (peer.Data as PlayerInfo).effect = 32;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2218)
                                                                { // icerod 
                                                                    (peer.Data as PlayerInfo).effect = 33;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2266)
                                                                { // glave
                                                                    (peer.Data as PlayerInfo).effect = 35;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2388)
                                                                { // hammer
                                                                    (peer.Data as PlayerInfo).effect = 37;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2450)
                                                                { // diamond dragon 
                                                                    (peer.Data as PlayerInfo).effect = 38;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2592)
                                                                { // katana
                                                                    (peer.Data as PlayerInfo).effect = 43;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2720)
                                                                { // ebow
                                                                    (peer.Data as PlayerInfo).effect = 44;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2756)
                                                                { // gungir
                                                                    (peer.Data as PlayerInfo).effect = 47;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                if (item == 2876)
                                                                { // gungir
                                                                    (peer.Data as PlayerInfo).effect = 51;
                                                                    sendState(peer); //here
                                                                    effect(peer, (peer.Data as PlayerInfo).effect);
                                                                }
                                                                (peer.Data as PlayerInfo).cloth_hand = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.BACK:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_back == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_back = 0;
                                                                    (peer.Data as PlayerInfo).canDoubleJump = false;
                                                                    sendState(peer);
                                                                    break;
                                                                }
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_back = pMov.plantingTree;
                                                                    int item = pMov.plantingTree;
                                                                    if (item == 156 || item == 362 || item == 678 || item == 736 ||
                                                                        item == 818 || item == 1206 || item == 1460 || item == 1550 ||
                                                                        item == 1574 || item == 1668 || item == 1672 || item == 1674 ||
                                                                        item == 1784 || item == 1824 || item == 1936 || item == 1938 ||
                                                                        item == 1970 || item == 2254 || item == 2256 || item == 2258 ||
                                                                        item == 2260 || item == 2262 || item == 2264 || item == 2390 ||
                                                                        item == 2392 || item == 3120 || item == 3308 || item == 3512 ||
                                                                        item == 4534 || item == 4986 || item == 5754 || item == 6144 ||
                                                                        item == 6334 || item == 6694 || item == 6818 || item == 6842 ||
                                                                        item == 1934 || item == 3134 || item == 6004 || item == 1780 ||
                                                                        item == 2158 || item == 2160 || item == 2162 || item == 2164 ||
                                                                        item == 2166 || item == 2168 || item == 2438 || item == 2538 ||
                                                                        item == 2778 || item == 3858 || item == 350 || item == 998 ||
                                                                        item == 1738 || item == 2642 || item == 2982 || item == 3104 ||
                                                                        item == 3144 || item == 5738 || item == 3112 || item == 2722 ||
                                                                        item == 3114 || item == 4970 || item == 4972 || item == 5020 ||
                                                                        item == 6284 || item == 4184 || item == 4628 || item == 5322 ||
                                                                        item == 4112 || item == 4114 || item == 3442)
                                                                    {
                                                                        (peer.Data as PlayerInfo).canDoubleJump = true;
                                                                        sendState(peer);
                                                                    }
                                                                    else
                                                                    {
                                                                        (peer.Data as PlayerInfo).canDoubleJump = false;
                                                                        sendState(peer);
                                                                    }

                                                                    // ^^^^ wings
                                                                    sendState(peer);
                                                                }
                                                                break;
                                                            }

                                                        case ClothTypes.MASK:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_mask == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_mask = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_mask = pMov.plantingTree;
                                                                break;
                                                            }

                                                        case ClothTypes.NECKLACE:
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_necklace == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_necklace = 0;
                                                                    break;
                                                                }
                                                            (peer.Data as PlayerInfo).cloth_necklace = pMov.plantingTree;
                                                                break;
                                                            }

                                                        default:
                                                            if (
                                                                    def.id == 7166
                                                                    || def.id == 5078 || def.id == 5080 || def.id == 5082 || def.id == 5084
                                                                    || def.id == 5126 || def.id == 5128 || def.id == 5130 || def.id == 5132
                                                                    || def.id == 5144 || def.id == 5146 || def.id == 5148 || def.id == 5150
                                                                    || def.id == 5162 || def.id == 5164 || def.id == 5166 || def.id == 5168
                                                                    || def.id == 5180 || def.id == 5182 || def.id == 5184 || def.id == 5186
                                                                    || def.id == 7168 || def.id == 7170 || def.id == 7172 || def.id == 7174
                                                                )
                                                            {
                                                                if ((peer.Data as PlayerInfo).cloth_ances == pMov.plantingTree)
                                                                {
                                                                    (peer.Data as PlayerInfo).cloth_ances = 0;
                                                                    break;
                                                                }
                                                                (peer.Data as PlayerInfo).cloth_ances = pMov.plantingTree;
                                                            }
                                                            {
                                                                Console.WriteLine("Invalid item activated: " + pMov.plantingTree + " by "
                                                                                  + (peer.Data as PlayerInfo).displayName);
                                                                break;
                                                            }
                                                    }

                                                    sendClothes(peer);
                                                    // activate item
                                                }

                                                if (data2.packetType == 18)
                                                {
                                                    sendPData(peer, pMov);
                                                    // add talk buble
                                                }

                                                if (data2.punchX != -1 && data2.punchY != -1)
                                                {
                                                    //cout << data2->packetType << endl;
                                                    if (data2.packetType == 3)
                                                    {
                                                        sendTileUpdate(data2.punchX, data2.punchY, data2.plantingTree,
                                                            (peer.Data as PlayerInfo).netID, peer);
                                                    }

                                                }

                                            }
                                            catch
                                            {
                                                Console.WriteLine("error in packettypes");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Got bad tank packet");
                                        }
                                    }
                                }
                                break;
                            case 5:
                                break;
                            case 6:
                            //cout << GetTextPointerFromPacket(event.packet) << endl;
                            break;
                        }
                    };
                    eve.Peer.OnDisconnect += (object send, uint ev) =>
                    {
                        ENetPeer peer = send as ENetPeer;
                        Console.WriteLine("Peer disconnected");
                        sendPlayerLeave(peer, peer.Data as PlayerInfo);
                        (peer.Data as PlayerInfo).inventory.items = new InventoryItem[] { };
                        peer.Data = null;
                        peers.Remove(peer);
                    };
                };
                server.StartServiceThread();
                Thread.Sleep(Timeout.Infinite);
                Console.WriteLine("Program ended??? Huh?");
            }
            catch
            {
                Console.WriteLine("[Warning: a retard trying to crash the server!]\nNOTE:if you see this when lunching the server then that mean python or xampp is closed and you dont have to worry about it!\nif not - Byko ?? raiter ??");
            }
        }
    }
}