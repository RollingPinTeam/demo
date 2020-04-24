using System.Collections.Generic;

public class UnitCfg
{
    public int id;
    public string name;
    public int hp;
    public int atk;
    public int def;
    
    private static Dictionary<int, UnitCfg> config = new Dictionary<int, UnitCfg>();
    
    public static void Init()
    {
        config.Add(1001, new UnitCfg() 
        {
            id = 1001,
            name = "女女",
            hp = 150,
            atk = 30,
        });

        config.Add(1002, new UnitCfg() 
        {
            id = 1002,
            name = "壮壮",
            hp = 200,
            atk = 20,
        });

        config.Add(1003, new UnitCfg() 
        {
            id = 1003,
            name = "男男",
            hp = 150,
            atk = 30,
        });

        config.Add(1004, new UnitCfg() 
        {
            id = 1004,
            name = "西西",
            hp = 100,
            atk = 50,
        });

        config.Add(2001, new UnitCfg() 
        {
            id = 2001,
            name = "近战M",
            hp = 100,
            atk = 30,
        });

        config.Add(2002, new UnitCfg() 
        {
            id = 1002,
            name = "远程M",
            hp = 50,
            atk = 50,
        });

        config.Add(2003, new UnitCfg() 
        {
            id = 2003,
            name = "Boss",
            hp = 300,
            atk = 60,
        });
    }

    public static UnitCfg Get(int id)
    {
        return config[id];
    }
}