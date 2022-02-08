using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg.Utils
{

    /// <summary>
    /// Clase para hacer operaciones utiles sobre arrays
    /// </summary>
    public class ArrayUtils

    {
        /// <summary>
        /// busca en un array un objeto en particular
        /// </summary>
        /// <param name="array">el array en el que buscar</param>
        /// <param name="target">el objeto que buscamos</param>
        /// <param name="start">el primer indice</param>
        /// <returns>el indice en el que se encuentra el objeto o -1 si no se ha encontrado</returns>
        public static int getIndex(object[] array, object target, int start = 0)
        {
            object found = null;
            int i = start ;
            while (found == null && i >= 0 && i < array.Length)
            {
                if (array[i].Equals(target))
                    found = array[i];
                i++;

            }
            return found == null ? -1 : i - 1;
        }

    }

}