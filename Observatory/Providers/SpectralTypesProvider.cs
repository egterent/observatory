using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Observatory.ObjectModel.Enums;

namespace Observatory.Providers
{
    class SpectralTypesProvider
    {
        public List<SpectralType> GetSpectralTypes()
        {
            var spectralTypes = new List<SpectralType> { SpectralType.A,
                SpectralType.B, SpectralType.F, SpectralType.G,
                SpectralType.K, SpectralType.M, SpectralType.O };
            return spectralTypes;
        }

    }
}
