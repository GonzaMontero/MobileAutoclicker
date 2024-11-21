using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Towers
{
    public abstract class Bullet
    {
        public abstract int ID { get; }

        public abstract void Process(Tower tower);
    }

    public class RegularBullet : Bullet
    {
        public override int ID => 1;

        public override void Process(Tower tower)
        {
            ObjectPooler.Get().EnableItem(1, out GameObject obj);

            obj.GetComponent<VisualBullet>().SetupBullet(tower.Target, tower.Damage);

            obj.transform.position = tower.FiringPoint.position;
            obj.transform.rotation = Quaternion.identity;
        }
    }

    public class IceBullet : Bullet
    {
        public override int ID => 2;

        public override void Process(Tower tower)
        {
            throw new System.NotImplementedException();
        }
    }

    public static class BulletsFactory
    {
        private static Dictionary<int, Type> bulletsByID;
        private static bool isInitialized => bulletsByID != null;

        private static void InitializeFactory()
        {
            if(isInitialized) 
                return;

            var bulletTypes = Assembly.GetAssembly(typeof(Bullet)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract
                                                    && myType.IsSubclassOf(typeof(Bullet)));

            bulletsByID = new Dictionary<int, Type>();

            foreach(var type in bulletTypes)
            {
                var tempBullet = Activator.CreateInstance(type) as Bullet;
                bulletsByID.Add(tempBullet.ID, type);
            }
        }

        public static Bullet GetBullet(int id)
        {
            InitializeFactory();

            if(bulletsByID.ContainsKey(id))
            {
                Type type = bulletsByID[id];
                var bullet = Activator.CreateInstance(type) as Bullet;
                return bullet;
            }

            return null;
        }

        internal static IEnumerable<int> GetBulletNames()
        {
            UnityEngine.Debug.Log("Test");

            InitializeFactory();

            return bulletsByID.Keys;
        }
    }
}