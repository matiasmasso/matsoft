using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Banca
{
    public class Norma43
    {
        public List<Registro> Registros { get; set; } = new();
        public List<Apunt> Apunts { get; set; } = new();

        public EmpModel.EmpIds? Emp { get; set; }


        public enum DHs
        {
            debe = 1,
            haber = 2
        }

        public enum ConceptesComuns
        {
            Reintegros = 1,
            Ingresos = 2,
            Domiciliaciones = 3,
            Transferencias = 4,
            Creditos = 5,
            RemesasEfectos = 6,
            Subscripciones = 7,
            Cupones = 8,
            Bolsa = 9,
            Gasolina = 10,
            CajeroAutomatico = 11,
            Visas = 12,
            Extranjero = 13,
            Impagos = 14,
            Nominas = 15,
            Timbres = 16,
            GastosBancarios = 17,
            Correcciones = 98,
            Varios = 99
        }

        public static Norma43 Factory(string src)
        {
            Norma43 retval = new();
            var lines = src.GetLines(removeEmptyLines: true);
            retval.Registros = lines.Select(x => Registro.Factory(x)).ToList();
            Apunt? currentApunt = null;
            CabeceraClass? cabecera = null;
            foreach (var registro in retval.Registros)
            {
                if (registro.Codi == Registro.Codis.Cabecera)
                {
                    cabecera = (CabeceraClass)registro;
                }
                if (registro.Codi == Registro.Codis.Movimiento)
                {
                    currentApunt = new Apunt();
                    currentApunt.SetRegistros(cabecera!, (Movimiento)registro);
                    retval.Apunts.Add(currentApunt);
                }
                else if (registro.Codi == Registro.Codis.Concepto)
                {
                    currentApunt!.RegsConcepto.Add((Concepto)registro);
                    currentApunt!.Concept += " " + ((Concepto)registro).FullConcept();
                }
                else if (registro.Codi == Registro.Codis.Complemento)
                {
                    currentApunt!.RegsComplemento.Add((Complemento)registro);
                    currentApunt!.Concept += " " + ((Complemento)registro).FullConcept();
                }
            }
            return retval;
        }

        public static bool Validate(byte[]? bytes)
        {
            string? src = null;
            if (bytes != null)
                src = System.Text.Encoding.UTF7.GetString(bytes);
            return Validate(src);
        }

        public static bool Validate(string? src)
        {
            bool retval = false;
            if (!string.IsNullOrEmpty(src))
            {
                var lines = src.GetLines(removeEmptyLines: true);
                var n = lines.Count;
                if (n > 2) //&& lines.All(x => x.Length == Registro.LENGTH))
                {
                    retval = true;
                    if (lines[0].Truncate(2) != "11") retval = false;
                    if (lines[1].Truncate(2) != "22") retval = false;
                    if (lines[n - 2].Truncate(2) != "33") retval = false;
                    if (lines[n - 1].Truncate(2) != "88") retval = false;
                }
                //var a = lines.Where(x => x.Length != 80).ToList();
            }
            return retval;
        }

        public CabeceraClass Cabecera() => (CabeceraClass)Registros.FirstOrDefault(x => x.Codi == Registro.Codis.Cabecera);
        public List<CccFch> CccFchs()
        {
            var retval = Apunts.Select(x => new CccFch
            {
                Ccc = x.FullCcc,
                Fch = x.Fch
            })
            .Distinct().ToList();
            return retval;
        }

        public class Template : BaseGuid
        {
            public EmpModel.EmpIds? Emp { get; set; }
            public string? Pattern { get; set; }
            public string? Concepte { get; set; }
            public Guid? Cta { get; set; }
            public Guid? Contact { get; set; }
            public Guid? Projecte { get; set; }

            public Template() : base() { }
            public Template(Guid guid) : base(guid) { }

            public override bool Matches(string? searchTerm)
            {
                bool retval = false;
                var searchTarget = Concepte + " " + Pattern;
                if (string.IsNullOrEmpty(searchTerm))
                    return true;
                else if (!string.IsNullOrEmpty(searchTarget))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }

            public override string ToString()
            {
                return Pattern ?? "?";
            }

        }

        public class Apunt : BaseGuid
        {
            public string? FullCcc { get; set; }
            public DateOnly? Fch { get; set; }
            public DateOnly? FchValor { get; set; }
            public DHs? Dh { get; set; }
            public Decimal Import { get; set; }
            public string? Concept { get; set; }
            public ConceptesComuns? ConceptoComun { get; set; }
            public string? ConceptoPropio { get; set; }

            public CabeceraClass? RegCabecera { get; set; }
            public Movimiento? RegMovimiento { get; set; }
            public List<Concepto> RegsConcepto { get; set; } = new();
            public List<Complemento> RegsComplemento { get; set; } = new();

            public List<Registro> Registros()
            {
                List<Registro> retval = new();
                retval.Add(RegMovimiento!);
                retval.AddRange(RegsConcepto);
                retval.AddRange(RegsComplemento);
                return retval;
            }

            public Apunt() : base() { }
            public Apunt(Guid guid) : base(guid) { }

            public Apunt(Guid guid, List<string> lines) : base(guid)
            {
                var registres = lines.Select(x => Registro.Factory(x)).ToList();
                CabeceraClass? cabecera = null;
                foreach (var registro in registres)
                {
                    if (registro.Codi == Registro.Codis.Cabecera)
                    {
                        cabecera = (CabeceraClass)registro;
                    }
                    if (registro.Codi == Registro.Codis.Movimiento)
                    {
                        SetRegistros(cabecera!, (Movimiento)registro);
                    }
                    else if (registro.Codi == Registro.Codis.Concepto)
                    {
                        RegsConcepto.Add((Concepto)registro);
                        Concept += " " + ((Concepto)registro).FullConcept();
                    }
                    else if (registro.Codi == Registro.Codis.Complemento)
                    {
                        RegsComplemento.Add((Complemento)registro);
                        Concept += " " + ((Complemento)registro).FullConcept();
                    }
                }
            }

            public Apunt(string ccc, Guid guid, List<string> lines) : base(guid)
            {
                FullCcc = ccc;
                var registres = lines.Select(x => Registro.Factory(x)).ToList();

                foreach (var registro in registres)
                {
                    if (registro.Codi == Registro.Codis.Movimiento)
                    {
                        SetMovimiento((Movimiento)registro);
                    }
                    else if (registro.Codi == Registro.Codis.Concepto)
                    {
                        RegsConcepto.Add((Concepto)registro);
                        Concept += " " + ((Concepto)registro).FullConcept();
                    }
                    else if (registro.Codi == Registro.Codis.Complemento)
                    {
                        RegsComplemento.Add((Complemento)registro);
                        Concept += " " + ((Complemento)registro).FullConcept();
                    }
                }
            }

            public void SetMovimiento(Movimiento src)
            {
                RegMovimiento = src;
                Fch = src.Fch;
                FchValor = src.FchValor;
                Dh = src.Dh;
                Import = src.Importe ?? 0;
                ConceptoComun = src.ConceptoComun;
                ConceptoPropio = src.ConceptoPropio;
                Concept = src.FullConcept();
            }

            public void SetRegistros(CabeceraClass cabecera, Movimiento src)
            {
                FullCcc = cabecera.FullCcc();
                //RegCabecera = cabecera;
                SetMovimiento(src);
            }

            public Apunt(Guid guid, CabeceraClass cabecera, Movimiento src) : base(guid)
            {
                SetRegistros(cabecera, src);
            }

            public string Referencias()
            {
                List<string?> refs = new() { RegMovimiento?.Referencia1, RegMovimiento?.Referencia2 };
                var retval = string.Join(", ", refs.Where(x => !string.IsNullOrEmpty(x)));
                return retval;
            }
            public string Conceptos()
            {
                List<string?> tmp = new();
                foreach (var reg in RegsConcepto)
                {
                    tmp.AddRange(new string?[] { reg.Concepto1, reg.Concepto2 });
                }
                var retval = string.Join(", ", tmp.Where(x => !string.IsNullOrEmpty(x)));
                return retval;
            }
            public string Complementos()
            {
                List<string?> tmp = new();
                foreach (var reg in RegsComplemento)
                {
                    if (reg.Importe != 0)
                        tmp.Add($"{reg.Importe:N2}");
                }
                var retval = string.Join(", ", tmp.Where(x => !string.IsNullOrEmpty(x)));
                return retval;
            }

            public override bool Matches(string? searchTerm)
            {
                bool retval = false;
                var searchTarget = Concept;
                if (string.IsNullOrEmpty(searchTerm))
                    return true;
                else if (!string.IsNullOrEmpty(searchTarget))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }

        }

        public class Registro
        {
            public const int LENGTH = 80;
            public Codis Codi { get; set; }
            public string Src { get; set; }

            public enum Codis
            {
                Cabecera = 11,
                Movimiento = 22,
                Concepto = 23,
                Complemento = 24,
                CheckSum = 33,
                EndOfFile = 88
            }

            public enum Divisas
            {
                EUR = 978,
                USD = 840,
                GBP = 826,
                CHF = 756
            }




            public static Registro Factory(string src)
            {
                Registro retval;
                switch (src.Truncate(2).ToEnum<Codis>())
                {
                    case Codis.Cabecera:
                        retval = CabeceraClass.Factory(src);
                        break;
                    case Codis.Movimiento:
                        retval = Movimiento.Factory(src);
                        break;
                    case Codis.Concepto:
                        retval = Concepto.Factory(src);
                        break;
                    case Codis.Complemento:
                        retval = Complemento.Factory(src);
                        break;
                    case Codis.CheckSum:
                        retval = CheckSum.Factory(src);
                        break;
                    case Codis.EndOfFile:
                        retval = EndOfFile.Factory(src);
                        break;
                    default:
                        throw new Exception("Registre norma 43 amb codi desconegut");
                }
                return retval;
            }
        }

        public class CabeceraClass : Registro
        {
            public string? Entidad { get; set; }
            public string? Oficina { get; set; }
            public string? Cuenta { get; set; }

            public DateTime? FchFrom { get; set; }
            public DateTime? FchTo { get; set; }

            public Decimal? SaldoFrom { get; set; }
            public Divisas Divisa { get; set; }

            public InfoModes? InfoMode { get; set; }

            public string? Titular { get; set; }

            public enum InfoModes
            {
                Uno = 1,
                Dos = 2,
                Tres = 3
            }
            public static new CabeceraClass Factory(string src)
            {
                var divisa = src.Truncate(47, 3);
                return new()
                {
                    Src = src,
                    Codi = Codis.Cabecera,
                    Entidad = src.Truncate(2, 4),
                    Oficina = src.Truncate(6, 4),
                    Cuenta = src.Truncate(10, 10),
                    FchFrom = src.Truncate(20, 6).ToDate(),
                    FchTo = src.Truncate(26, 6).ToDate(),
                    SaldoFrom = src.Truncate(33, 14).ParseDecimal() / 100,
                    Divisa = src.Truncate(47, 3).ToEnum<Divisas>(),
                    InfoMode = src.Truncate(50, 1).ToEnum<InfoModes>(),
                    Titular = src.Truncate(51, 26)
                };
            }

            public string FullCcc() => $"{Entidad}{Oficina}{Cuenta}";


        }
        public class Movimiento : Registro
        {
            public string? Oficina { get; set; }
            public DateOnly? Fch { get; set; }
            public DateOnly? FchValor { get; set; }
            public ConceptesComuns? ConceptoComun { get; set; }
            public string? ConceptoPropio { get; set; }

            public DHs? Dh { get; set; }
            public Divisas? Divisa { get; set; }
            public Decimal? Importe { get; set; }

            public string? DocNum { get; set; }
            public string? Referencia1 { get; set; }
            public string? Referencia2 { get; set; }

            public static new Movimiento Factory(string src)
            {
                Movimiento retval = new();
                retval.Src = src;
                retval.Codi = Codis.Movimiento;

                return new()
                {
                    Src = src,
                    Codi = Codis.Movimiento,
                    Oficina = src.Truncate(6, 4),
                    Fch = src.Truncate(10, 6).ToDateOnly(),
                    FchValor = src.Truncate(16, 6).ToDateOnly(),
                    ConceptoComun = src.Truncate(22, 2).ToInteger().ToString()?.ToEnum<ConceptesComuns>(),
                    ConceptoPropio = src.Truncate(24, 3),
                    Dh = src.Truncate(27, 1).ToEnum<DHs>(),
                    Importe = src.Truncate(28, 14).ParseDecimal() / 100,
                    DocNum = src.Truncate(42, 20) == "00000000000000000000" ? string.Empty : src.Truncate(42, 20).Trim(),
                    Referencia1 = src.Truncate(52, 12) == "000000000000" ? string.Empty : src.Truncate(52, 12).Trim(),
                    Referencia2 = src.Truncate(64, 16) == "0000000000000000" ? string.Empty : src.Truncate(64, 16).Trim()
                };

            }

            public string? FullConcept()
            {
                var segments = new List<string?>() { DocNum, Referencia1, Referencia2 };
                var retval = String.Join(" ", segments.Where(s => !string.IsNullOrEmpty(s)));
                return retval;
            }
        }
        public class Concepto : Registro
        {
            public int Idx { get; set; }
            public string? Concepto1 { get; set; }
            public string? Concepto2 { get; set; }
            public static new Concepto Factory(string src)
            {
                return new()
                {
                    Src = src,
                    Codi = Codis.Concepto,
                    Idx = src.Truncate(2, 2).ToInteger() ?? 0,
                    Concepto1 = src.Truncate(4, 38).Trim(),
                    Concepto2 = src.Truncate(42, 38).Trim()
                };
            }

            public string? FullConcept()
            {
                var segments = new List<string?>() { Concepto1, Concepto2 };
                var retval = String.Join(" ", segments.Where(s => !string.IsNullOrEmpty(s)));
                retval = retval.Replace("Fecha de operación:", "").Replace("OPERACIO TARGETA", "VISA");
                return retval;
            }

        }

        public class Complemento : Registro
        {
            public int Idx { get; set; }
            public Divisas Divisa { get; set; }
            public Decimal? Importe { get; set; }
            public static new Complemento Factory(string src)
            {
                return new()
                {
                    Src = src,
                    Codi = Codis.Complemento,
                    Idx = src.Truncate(2, 2).ToInteger() ?? 0,
                    Divisa = src.Truncate(4, 3).ToEnum<Divisas>(),
                    Importe = src.Truncate(7, 14).ParseDecimal() / 100
                };
            }

            public string? FullConcept()
            {
                string? retval = null;
                if (Importe != null)
                {
                    retval = ((Decimal)Importe).ToString("N2");
                }
                return retval;
            }
        }
        public class CheckSum : Registro
        {
            public string? Entidad { get; set; }
            public string? Oficina { get; set; }
            public string? Cuenta { get; set; }
            public int DebeCount { get; set; }
            public decimal DebeSum { get; set; }
            public int HaberCount { get; set; }
            public decimal HaberSum { get; set; }
            public DHs SignoSaldoFinal { get; set; }
            public decimal SaldoFinal { get; set; }
            public Divisas Divisa { get; set; }
            public static new CheckSum Factory(string src)
            {
                return new()
                {
                    Src = src,
                    Codi = Codis.CheckSum,
                    Entidad = src.Truncate(2, 4),
                    Oficina = src.Truncate(6, 4),
                    Cuenta = src.Truncate(10, 10),
                    DebeCount = src.Truncate(20, 5).ToInteger() ?? 0,
                    DebeSum = src.Truncate(25, 14).ParseDecimal() / 100 ?? 0,
                    HaberCount = src.Truncate(39, 5).ToInteger() ?? 0,
                    HaberSum = src.Truncate(44, 14).ParseDecimal() / 100 ?? 0,
                    SignoSaldoFinal = src.Truncate(58, 1).ToEnum<DHs>(),
                    SaldoFinal = src.Truncate(59, 14).ParseDecimal() / 100 ?? 0,
                    Divisa = src.Truncate(73, 3).ToEnum<Divisas>()
                };
            }
        }
        public class EndOfFile : Registro
        {
            public int RegistrosCount { get; set; }
            public static new EndOfFile Factory(string src)
            {
                return new()
                {
                    Src = src,
                    Codi = Codis.EndOfFile,
                    RegistrosCount = src.Truncate(20, 6).ToInteger() ?? 0
                };
            }
        }

        public class CccFch : BaseGuid
        {
            public string Ccc { get; set; } = "";
            public DateOnly? Fch { get; set; }


            public CccFch() : base() { }
            public CccFch(Guid guid) : base(guid) { }


            public object Distinct()
            {
                throw new NotImplementedException();
            }

            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                var candidate = obj as CccFch;
                if (candidate == null) return false;
                if (candidate.Ccc != Ccc) return false;
                if (candidate.Fch != Fch) return false;
                return true;
            }

            public override int GetHashCode()
            {
                int hash = 23;
                hash = hash * 59 + (Ccc == null ? 0 : Ccc.GetHashCode());
                hash = hash * 59 + Fch.GetHashCode();
                return hash;
            }
        }
    }
}
