using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOPgcClass : DTOBaseGuid
    {
        public DTOPgcPlan Plan { get; set; }
        public DTOPgcClass Parent { get; set; }
        public int Ord { get; set; }
        public DTOLangText Nom { get; set; }
        public Levels Level { get; set; }

        public List<DTOPgcClass> Children { get; set; }
        public List<DTOPgcCta> Ctas { get; set; }
        public decimal CurrentDeb { get; set; }
        public decimal CurrentHab { get; set; }
        public decimal PreviousDeb { get; set; }
        public decimal PreviousHab { get; set; }
        public Cods Cod { get; set; }
        public bool HideFigures { get; set; }

        public List<DTOPgcClass.Cods> Sumandos { get; set; }
        public List<DTOYearMonth> YearMonths { get; set; }

        public enum CodsOld
        {
            NotSet,
            Activo,
            Pasivo,
            PG,
            Resultado,
            A1_ResultadoExplotacion,
            A2_ResultadoFinanciero,
            A3_ResultadoAntesDeImpuestos,
            ImpuestoBeneficios,
            A4_ResultadoOpsContinuadas,
            B_OperacionesInterrumpidas,
            ResultadoOpsInterrumpidas,
            A5_ResultadoDelEjercicio
        }
        public enum Cods
        {
            a_Balance,
            aA_Activo,
            aAA_Activo_No_Corriente,
            aAA1_Inmovilizado_intangible,
            aAA11_Desarrollo,
            aAA12_Concesiones,
            aAA13_Patentes,
            aAA14_Fondo_de_Comercio,
            aAA15_Software,
            aAA16_Investigacion,
            aAA17_Propiedad_Intelectual,
            aAA18_Derechos_emision_gases,
            aAA19_Otro_Inmovil_Intangible,
            aAA2_Inmovilizado_material,
            aAA21_Terrenos_y_Construcciones,
            aAA22_Instalaciones,
            aAA23_Inmovilizado_en_Curso,
            aAA3_Inversiones_Inmobiliarias,
            aAA31_Terrenos,
            aAA32_Construcciones,
            aAA4_Inversiones_Grupo,
            aAA41_Instrumentos_de_Patrimonio,
            aAA42_Creditos_a_Empresas,
            aAA43_Valores_Representativos_de_Deuda,
            aAA44_Derivados,
            aAA45_Otros_Activos_Financieros,
            aAA46_Otras_Inversiones,
            aAA5_Inversiones_Financieras_LP,
            aAA51_Instrumentos_de_Patrimonio,
            aAA52_Creditos_a_terceros,
            aAA53_Valores_Representativos_de_Deuda,
            aAA54_Derivados,
            aAA55_Otros_Activos_Financieros,
            aAA56_Otras_Inversiones,
            aAA6_Activos_por_impuesto_diferido,
            aAA7_Deudas_Comerciales_no_corrientes,
            aAB_Activo_Corriente,
            aAB1_Activos_no_corrientes_a_la_venta,
            aAB2_Existencias,
            aAB21_Existencias_Comerciales,
            aAB22_Materias_Primas,
            aAB23_Productos_en_curso,
            aAB24_Productos_terminados,
            aAB25_Subproductos,
            aAB26_Anticipos_a_Proveedores,
            aAB3_Deudores_comerciales,
            aAB31_Clientes,
            aAB32_Clientes_Grupo,
            aAB33_Deudores_varios,
            aAB34_Personal,
            aAB35_Activos_por_Impuesto_Corriente,
            aAB36_Otros_creditos_Adm_Pub,
            aAB37_Accionistas_por_desembolsos_exigidos,
            aAB4_Inversiones_en_empresas_Grupo,
            aAB41_Instrumentos_de_Patrimonio,
            aAB42_Creditos_a_Empresas,
            aAB43_Valores_Representativos_de_Deuda,
            aAB44_Derivados,
            aAB45_Otros_Activos_Financieros,
            aAB46_Otras_Inversiones,
            aAB5_Inversiones_financieras_a_corto,
            aAB51_Instrumentos_de_Patrimonio,
            aAB52_Creditos_a_Empresas,
            aAB53_Valores_Representativos_de_Deuda,
            aAB54_Derivados,
            aAB55_Otros_Activos_Financieros,
            aAB56_Otras_Inversiones,
            aAB6_Periodificaciones_a_corto,
            aAB7_Efectivo,
            aAB71_Tesoreria,
            aAB72_Otros_activos_liquidos,
            aB_Pasivo,
            aBA_Patrimonio_Neto,
            aBA1_Fondos_Propios,
            aBA11_Capital,
            aBA111_Capital_Escriturado,
            aBA112_Capital_no_exigido,
            aBA12_Prima_de_emision,
            aBA13_Reservas,
            aBA131_Reservas_legales,
            aBA132_Reservas_voluntarias,
            aBA133_Reserva_de_Revalorizacion,
            aBA14_Autocartera,
            aBA15_Resultados_Ejercicios_anteriores,
            aBA151_Remanente,
            aBA152_Resultados_negativos_ejercicios_anteriores,
            aBA16_Otras_aportaciones_de_socios,
            aBA17_Resultado_del_ejercicio,
            aBA18_Dividemdo_a_cuenta,
            aBA19_Otros_instr_patr_neto,
            aBA2_Ajustes_por_cambios_de_valor,
            aBA21_Activos_financieros_a_la_venta,
            aBA22_Operaciones_de_cobertura,
            aBA23_Activos_no_corrientes,
            aBA24_Diferencia_de_Conversion,
            aBA25_Otros_ajustes,
            aBA3_Subvenciones,
            aBB_Pasivo_No_Corriente,
            aBB1_Provisiones_LP,
            aBB11_Obligaciones_personal,
            aBB12_Medioambiental,
            aBB13_Reestructuracion,
            aBB14_Otras_provisiones,
            aBB2_Deudas_LP,
            aBB21_Obligaciones,
            aBB22_Deudas_con_Entidades_de_credito,
            aBB23_Leasing,
            aBB24_Derivados,
            aBB25_Otros_pasivos_financieros,
            aBB3_Deudas_grupo,
            aBB4_Pasivos_impuesto_diferido,
            aBB5_Periodificaciones_LP,
            aBB6_Acreedores_comerciales_no_corrientes,
            aBB7_Deuda_especial_LP,
            aBC_Pasivo_Corriente,
            aBC1_Vinculados_a_act_no_corr_a_la_venta,
            aBC2_Provisiones_CP,
            aBC21_Gases_efto_invernadero,
            aBC22_Otras_provisiones,
            aBC3_Deudas_CP,
            aBC31_Obligaciones,
            aBC32_Deudas_CP_con_entidades_de_credito,
            aBC33_Leasing,
            aBC34_Derivados,
            aBC35_Otros_pasivos_financieros,
            aBC4_Deudas_Grupo_CP,
            aBC5_Acreedores,
            aBC51_Proveedores,
            aBC52_Proveedores_grupo,
            aBC53_Acreedores_varios,
            aBC54_Personal,
            aBC55_Pasivos_por_impuesto_corriente,
            aBC56_Administraciones_Publicas,
            aBC57_Anticipos_de_Clientes,
            aBC6_Periodificaciones_CP,
            aBC7_Deuda_especial_CP,
            b_Cuenta_Explotacion,
            bA_Operaciones_Continuadas,
            bA1_Resultado_de_explotacion,
            bA101_Turnover,
            bA101A_Ventas,
            bA101B_Servicios,
            bA101C,
            bA102_Variacion_Existencias,
            bA103_Trabajos,
            bA104_Aprovisionamientos,
            bA104a,
            bA104b,
            bA104c,
            bA104d,
            bA105_Otros_ingresos_de_explotacion,
            bA105a_Ingresos_accesorios,
            bA105b_Subvenciones,
            bA106_Personal,
            bA106a_Sueldos,
            bA106b_Cargas_sociales,
            bA106c_Provisiones,
            bA107_Otros_gastos_explotacion,
            bA107a_Servicios_exteriores,
            bA107b_Tributos,
            bA107c_Deterioro,
            bA107d_Otros,
            bA107e_Gases,
            bA108_Amortizaciones,
            bA109_Subvenciones,
            bA110_Exceso_de_Provisiones,
            bA111_Deterioro,
            bA111A,
            bA111b,
            bA111c,
            bA112,
            bA113,
            bA2_Resultado_Financiero,
            bA214_Ingresos_Financieros,
            bA214a,
            bA214b,
            bA214c,
            bA215_Gastos_Financieros,
            bA215a,
            bA215b,
            bA215c,
            bA216,
            bA216a,
            bA216b,
            bA217_Diferencias_de_cambio,
            bA218,
            bA218a,
            bA218b,
            bA219,
            bA219a,
            bA219b,
            bA219c,
            bA3_Resultado_antes_de_impuestos,
            bA320_Impuesto_sobre_beneficios,
            bA4_Resultado_de_operaciones_Continuadas,
            bB_Operaciones_Interrumpidas,
            bB21_Resultado_OI_neto_de_Impuestos,
            bA5_Resultado_del_ejercicio
        }


        public enum Levels
        {
            notSet,
            uno,
            dos,
            tres,
            cuatro,
            cinco,
            seis,
            siete
        }

        public DTOPgcClass() : base()
        {
            Children = new List<DTOPgcClass>();
            Ctas = new List<DTOPgcCta>();
            Sumandos = new List<DTOPgcClass.Cods>();
        }

        public DTOPgcClass(Guid oGuid) : base(oGuid)
        {
            Children = new List<DTOPgcClass>();
            Ctas = new List<DTOPgcCta>();
            Sumandos = new List<DTOPgcClass.Cods>();
        }

        public static DTOPgcClass Root(DTOPgcClass oClass)
        {
            while (oClass.Parent != null)
                oClass = oClass.Parent;
            return oClass;
        }

        public int GetLevel()
        {
            var retval = 0;
            var oParent = Parent;
            while (oParent != null)
            {
                retval += 1;
                oParent = oParent.Parent;
            }
            return retval;
        }

        public static decimal Eur(DTOPgcClass oClass, int year, int mes)
        {
            decimal retval = 0;

            if (oClass.Ctas != null)
            {
                if (oClass.YearMonths == null)
                {
                    foreach (var oCta in oClass.Ctas)
                    {
                        if (oCta.YearMonths != null)
                        {
                            var oYearMonth = oCta.YearMonths.FirstOrDefault(x => x.Year == year & (int)x.Month == mes);
                            if (oYearMonth != null)
                                retval += oYearMonth.Eur;
                        }
                    }
                }
                else
                {
                    var oYearMonth = oClass.YearMonths.FirstOrDefault(x => x.Year == year & (int)x.Month == mes);
                    if (oYearMonth != null)
                        retval += oYearMonth.Eur;
                }
            }

            foreach (var oChild in oClass.Children)
                retval += Eur(oChild, year, mes);
            return retval;
        }

        public static decimal Eur(List<DTOPgcClass> oClasses, DTOPgcClass.Cods oClassCod, int year, int mes)
        {
            DTOPgcClass oClass = DTOPgcClass.RecursiveSearch(oClasses, oClassCod);
            decimal retval = Eur(oClass, year, mes);
            return retval;
        }


        public static DTOPgcClass RecursiveSearch(List<DTOPgcClass> oNodes, DTOPgcClass.Cods oCod)
        {
            DTOPgcClass retval = null;
            foreach (DTOPgcClass oClass in oNodes)
            {
                if (oClass.Cod == oCod)
                {
                    retval = oClass;
                    break;
                }
                else if (oClass.Children.Count > 0)
                {
                    retval = RecursiveSearch(oClass.Children, oCod);
                    if (retval != null)
                        break;
                }
            }
            return retval;
        }

        public static DTOPgcClass RecursiveSearch(DTOPgcClass src, DTOPgcClass.Cods oCod)
        {
            DTOPgcClass retval = null;
            if (src.Cod == oCod)
                retval = src;
            else
                foreach (DTOPgcClass oChild in src.Children)
                {
                    DTOPgcClass tmp = RecursiveSearch(oChild, oCod);
                    if (tmp != null)
                    {
                        retval = tmp;
                        break;
                    }
                }
            return retval;
        }

        public static List<DTOPgcClass> Tree(List<DTOPgcClass> src)
        {
            List<DTOPgcClass> retval = new List<DTOPgcClass>();
            foreach (var item in src)
            {
                // If item.Cod = 87 Then Stop
                if (item.Parent == null)
                    retval.Add(item);
                else
                {
                    DTOPgcClass oParent = src.Find(x => x.Guid.Equals(item.Parent.Guid));
                    if (oParent != null)
                    {
                        item.Parent = oParent;
                        oParent.Children.Add(item);
                    }
                }
            }
            return retval;
        }

        public static DTOPgcClass TreeSearch(List<DTOPgcClass> oTree, DTOPgcClass.Cods oCod)
        {
            var retval = oTree.FirstOrDefault(x => x.Cod == oCod);
            if (retval == null)
            {
                foreach (var item in oTree)
                {
                    if (item.Children != null)
                    {
                        retval = TreeSearch(item.Children, oCod);
                        if (retval != null)
                            break;
                    }
                }
            }
            return retval;
        }

        public static void SetLevels(List<DTOPgcClass> values)
        {
            foreach (DTOPgcClass item in values)
            {
                if (item.Parent != null)
                    item.Level = item.Parent.Level + 1;
                SetLevels(item.Children);
            }
        }

        public static void CalcSumandos(ref List<DTOPgcClass> values)
        {
            foreach (DTOPgcClass item in values)
            {
                if (item.Sumandos.Count > 0)
                {
                    item.CurrentDeb = 0;
                    item.CurrentHab = 0;
                    item.PreviousDeb = 0;
                    item.PreviousHab = 0;

                    foreach (DTOPgcClass.Cods oCod in item.Sumandos)
                    {
                        // Dim oSumando As DTOPgcClass = values.Find(Function(x) x.Cod.Equals(oCod))
                        DTOPgcClass oSumando = DTOPgcClass.RecursiveSearch(values, oCod);
                        if (oSumando != null)
                        {
                            {
                                var withBlock = oSumando;
                                item.CurrentDeb += withBlock.CurrentDeb;
                                item.CurrentHab += withBlock.CurrentHab;
                                item.PreviousDeb += withBlock.PreviousDeb;
                                item.PreviousHab += withBlock.PreviousHab;
                            }
                        }
                    }
                }
            }
        }

        public static void ReverseSignOnAssets(DTOPgcClass oActiu)
        {
            if (oActiu.Ctas.Count == 0)
            {
                foreach (var oChild in oActiu.Children)
                    ReverseSignOnAssets(oChild);
            }
            else
                foreach (var oCta in oActiu.Ctas)
                {
                    foreach (DTOYearMonth oYearMonth in oCta.YearMonths)
                        oYearMonth.Eur = -oYearMonth.Eur;
                }
        }

        public static void SetResultats(List<DTOPgcClass> oTree, int FromYear)
        {
            SetResultadoDelEjercicio(oTree, FromYear);
            SetResultadoAntesDeImpuestos(oTree, FromYear);
        }

        public static void SetResultadoDelEjercicio(List<DTOPgcClass> oTree, int FromYear)
        {
            DTOPgcClass oActiu = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aA_Activo);
            DTOPgcClass oPassiu = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aB_Pasivo);
            DTOPgcClass oResultats = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aBA17_Resultado_del_ejercicio);
            DTOPgcCta oCta129 = oResultats.Ctas.FirstOrDefault(x => x.Codi == DTOPgcPlan.Ctas.ResultatsAnyCorrent);
            oCta129.YearMonths = new List<DTOYearMonth>();
            for (int Year = FromYear; Year <= DTO.GlobalVariables.Today().Year; Year++)
            {
                for (int mes = 1; mes <= 12; mes++)
                {
                    decimal dcActiu = DTOPgcClass.Eur(oActiu, Year, mes);
                    decimal dcPassiu = DTOPgcClass.Eur(oPassiu, Year, mes);
                    var dcResultat = dcActiu - dcPassiu;
                    var oYearMonth = new DTOYearMonth(Year, (DTOYearMonth.Months)mes, dcResultat);
                    oCta129.YearMonths.Add(oYearMonth);
                }
            }
        }

        public static void SetResultadoDeExplotacion(List<DTOPgcClass> oTree, int FromYear)
        {
            var oPLBA101 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA101_Turnover);
            var oPLBA102 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA102_Variacion_Existencias);
            var oPLBA103 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA103_Trabajos);
            var oPLBA104 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA104_Aprovisionamientos);
            var oPLBA105 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA105_Otros_ingresos_de_explotacion);
            var oPLBA106 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA106_Personal);
            var oPLBA107 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA107_Otros_gastos_explotacion);
            var oPLBA108 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA108_Amortizaciones);
            var oPLBA109 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA109_Subvenciones);
            var oPLBA110 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA110_Exceso_de_Provisiones);
            var oPLBA111 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA111_Deterioro);
            var oPLBA112 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA112);
            var oPLBA113 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA113);

            var oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA1_Resultado_de_explotacion);
            oPgcClass.YearMonths = new List<DTOYearMonth>();
            for (int Year = FromYear; Year <= DTO.GlobalVariables.Today().Year; Year++)
            {
                for (int mes = 1; mes <= 12; mes++)
                {
                    decimal dcPLBA101 = DTOPgcClass.Eur(oPLBA101, Year, mes);
                    decimal dcPLBA102 = DTOPgcClass.Eur(oPLBA102, Year, mes);
                    decimal dcPLBA103 = DTOPgcClass.Eur(oPLBA103, Year, mes);
                    decimal dcPLBA104 = DTOPgcClass.Eur(oPLBA104, Year, mes);
                    decimal dcPLBA105 = DTOPgcClass.Eur(oPLBA105, Year, mes);
                    decimal dcPLBA106 = DTOPgcClass.Eur(oPLBA106, Year, mes);
                    decimal dcPLBA107 = DTOPgcClass.Eur(oPLBA107, Year, mes);
                    decimal dcPLBA108 = DTOPgcClass.Eur(oPLBA108, Year, mes);
                    decimal dcPLBA109 = DTOPgcClass.Eur(oPLBA109, Year, mes);
                    decimal dcPLBA110 = DTOPgcClass.Eur(oPLBA110, Year, mes);
                    decimal dcPLBA111 = DTOPgcClass.Eur(oPLBA111, Year, mes);
                    decimal dcPLBA112 = DTOPgcClass.Eur(oPLBA112, Year, mes);
                    decimal dcPLBA113 = DTOPgcClass.Eur(oPLBA113, Year, mes);
                    var dcEur = dcPLBA101 + dcPLBA102 + dcPLBA103 + dcPLBA104 + dcPLBA105 + dcPLBA106 + dcPLBA107 + dcPLBA108 + dcPLBA109 + dcPLBA110 + dcPLBA111 + dcPLBA112 + dcPLBA113;
                    var oYearMonth = new DTOYearMonth(Year, (DTOYearMonth.Months)mes, dcEur);
                    oPgcClass.YearMonths.Add(oYearMonth);
                    if (Year == 2017 & mes == 1)
                        System.Diagnostics.Debugger.Break();
                }
            }
        }

        public static void SetResultadoFinanciero(List<DTOPgcClass> oTree, int FromYear)
        {
            var oPLBA214 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA214_Ingresos_Financieros);
            var oPLBA215 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA215_Gastos_Financieros);
            var oPLBA216 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA216);
            var oPLBA217 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA217_Diferencias_de_cambio);
            var oPLBA218 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA218);
            var oPLBA219 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA219);

            var oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA2_Resultado_Financiero);
            oPgcClass.YearMonths = new List<DTOYearMonth>();
            for (int Year = FromYear; Year <= DTO.GlobalVariables.Today().Year; Year++)
            {
                for (int mes = 1; mes <= 12; mes++)
                {
                    decimal dcPLBA214 = DTOPgcClass.Eur(oPLBA214, Year, mes);
                    decimal dcPLBA215 = DTOPgcClass.Eur(oPLBA215, Year, mes);
                    decimal dcPLBA216 = DTOPgcClass.Eur(oPLBA216, Year, mes);
                    decimal dcPLBA217 = DTOPgcClass.Eur(oPLBA217, Year, mes);
                    decimal dcPLBA218 = DTOPgcClass.Eur(oPLBA218, Year, mes);
                    decimal dcPLBA219 = DTOPgcClass.Eur(oPLBA219, Year, mes);
                    var dcEur = dcPLBA214 + dcPLBA215 + dcPLBA216 + dcPLBA217 + dcPLBA218 + dcPLBA219;
                    var oYearMonth = new DTOYearMonth(Year, (DTOYearMonth.Months)mes, dcEur);
                    oPgcClass.YearMonths.Add(oYearMonth);
                }
            }
        }

        public static void SetResultadoAntesDeImpuestos(List<DTOPgcClass> oTree, int FromYear)
        {
            var oPLBA1 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA1_Resultado_de_explotacion);
            var oPLBA2 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA2_Resultado_Financiero);

            var oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA3_Resultado_antes_de_impuestos);
            oPgcClass.YearMonths = new List<DTOYearMonth>();
            for (int Year = FromYear; Year <= DTO.GlobalVariables.Today().Year; Year++)
            {
                for (int mes = 1; mes <= 12; mes++)
                {
                    decimal dcResultatExplotacio = DTOPgcClass.Eur(oPLBA1, Year, mes);
                    decimal dcResultatFinancer = DTOPgcClass.Eur(oPLBA2, Year, mes);
                    var dcEur = dcResultatExplotacio + dcResultatFinancer;
                    var oYearMonth = new DTOYearMonth(Year, (DTOYearMonth.Months)mes, dcEur);
                    oPgcClass.YearMonths.Add(oYearMonth);
                }
            }
        }
    }
}
