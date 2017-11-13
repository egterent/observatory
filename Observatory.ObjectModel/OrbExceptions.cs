using System;

using Observatory.ObjectModel.Enums;


namespace Observatory.ObjectModel
{
    public static class OrbExceptions
    {
        public static void OrbNameArgumentException(string orbName, string orbType)
        {
            if (orbName == string.Empty)
            {
                string s = string.Concat
                    ("Наименование ", orbType, " не может быть пустой строкой");
                throw new ArgumentException(s);
            }
        }        

        public static void OrbCharsArgumentException(SpectralType spectralType)
        {
            if (!Enum.IsDefined(typeof(SpectralType), spectralType))
                throw new ArgumentOutOfRangeException
                    ("Возможное значение спектрального класса: A, B, F, G, K, M, O");
        }

        public static void OrbCharsArgumentException
            (float objectCharValue, string objectTypeAndCharName)
        {           
            if (objectCharValue < 0)
            {
                string s = string.Concat(" - недопустимое значение ", objectTypeAndCharName);
                string.Concat(objectCharValue.ToString(), s);
                throw new ArgumentException(s);
            }
        }

        public static void OrbFaildArgumentException
            (ObservableDictionary<int, Planet> planetDictionary, int id, 
            string planetType)
        {
            if (planetDictionary == null)
                throw new ArgumentNullException("В списке нет планет");
            if (!planetDictionary.ContainsKey(id))
            {
                var s = string.Concat(planetType, " с индексом ", 
                    id," отсутствует в списке" );
                throw new ArgumentException(s);
            }
        }

        public static void OrbFaildArgumentException
            (ObservableDictionary<int, Star> starDictionary, int id)
        {
            if (starDictionary == null)
                throw new ArgumentNullException("В списке нет звезд");
            if (!starDictionary.ContainsKey(id))
            {
                var s = string.Concat("Звезда с индексом ",
                    id, " отсутствует в списке");
                throw new ArgumentException(s);
            }
        }

        public static void OrbFaildArgumentException
            (ObservableDictionary<int, Galaxy> galaxyDictionary, int id)
        {
            if (galaxyDictionary == null)
                throw new ArgumentNullException("В списке нет галактик");
            if (!galaxyDictionary.ContainsKey(id))
            {
                var s = string.Concat("Галактика с индексом ",
                    id, " отсутствует в списке");
                throw new ArgumentException(s);
            }
        }

        public static void OrbIdDetectedArgumentException
            (ObservableDictionary<int, Planet> planetDictionary, int id,
            string planetType)
        {
            if (planetDictionary.ContainsKey(id))
            {
                var s = string.Concat(planetType, " с id ",
                        id, " была добавлена в список ранее");
                throw new ArgumentException(s);
            }            
        }

        public static void OrbIdDetectedArgumentException
            (ObservableDictionary<int, Star> starDictionary, int id)
        {
            if (starDictionary.ContainsKey(id))
            {
                var s = string.Concat("Звезда с id ", id, 
                    " была добавлена в список ранее");
                throw new ArgumentException(s);
            }
        }

        public static void OrbIdDetectedArgumentException
            (ObservableDictionary<int, Galaxy> galaxyDictionary, int id)
        {
            if (galaxyDictionary.ContainsKey(id))
            {
                var s = string.Concat("Галактика с id ", id,
                    " была добавлена в список ранее");
                throw new ArgumentException(s);
            }
        }

        public static void OrbNameDetectedArgumentException
            (ObservableDictionary<int, Planet> planetDictionary, string planetName,
            string planetType)
        {
            foreach (var x in planetDictionary.Values)
                if (x.Name == planetName)
                {
                    var s = string.Concat(planetType, " с наименованием ",
                        planetName, " была добавлена в список ранее");
                    throw new ArgumentException(s);
                }
        }

        public static void OrbNameDetectedArgumentException
            (ObservableDictionary<int, Star> starDictionary, string starName)
        {
            foreach (var x in starDictionary.Values)
                if (x.Name == starName)
                {
                    var s = string.Concat("Звезда с наименованием ",
                        starName, " была добавлена в список ранее");
                    throw new ArgumentException(s);
                }
        }

        public static void OrbNameDetectedArgumentException
            (ObservableDictionary<int, Galaxy> galaxyDictionary,
            string galaxyName)
        {
            foreach (var x in galaxyDictionary.Values)
                if (x.Name == galaxyName)
                {
                    var s = string.Concat("Галактика с наименованием ",
                        galaxyName, " была добавлена в список ранее");
                    throw new ArgumentException(s);
                }
        }

        public static void OrbNullArgumentException(object orb, string orbType)
        {
            if (orb == null)
            {
                var s = string.Concat("Значение для ",
                    orbType, " не может быть null");
                throw new ArgumentNullException(s);
            }
        }
    }
}
