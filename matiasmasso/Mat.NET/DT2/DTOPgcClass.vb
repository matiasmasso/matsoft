Public Class DTOPgcClass
    Inherits DTOBaseGuid

    Property Plan As DTOPgcPlan
    Property Parent As DTOPgcClass
    Property Ord As Integer
    Property Nom As DTOLangText
    Property Level As Levels

    Property Children As List(Of DTOPgcClass)
    Property Ctas As List(Of DTOPgcCta)
    Property CurrentDeb As Decimal
    Property CurrentHab As Decimal
    Property PreviousDeb As Decimal
    Property PreviousHab As Decimal
    Property Cod As Cods
    Property HideFigures As Boolean

    Property Sumandos As List(Of DTOPgcClass.Cods)
    Property YearMonths As List(Of DTOYearMonth)

    Public Enum CodsOld
        NotSet
        Activo
        Pasivo
        PG
        Resultado
        A1_ResultadoExplotacion
        A2_ResultadoFinanciero
        A3_ResultadoAntesDeImpuestos
        ImpuestoBeneficios
        A4_ResultadoOpsContinuadas
        B_OperacionesInterrumpidas
        ResultadoOpsInterrumpidas
        A5_ResultadoDelEjercicio
    End Enum
    Public Enum Cods
        a_Balance
        aA_Activo
        aAA_Activo_No_Corriente
        aAA1_Inmovilizado_intangible
        aAA11_Desarrollo
        aAA12_Concesiones
        aAA13_Patentes
        aAA14_Fondo_de_Comercio
        aAA15_Software
        aAA16_Investigacion
        aAA17_Propiedad_Intelectual
        aAA18_Derechos_emision_gases
        aAA19_Otro_Inmovil_Intangible
        aAA2_Inmovilizado_material
        aAA21_Terrenos_y_Construcciones
        aAA22_Instalaciones
        aAA23_Inmovilizado_en_Curso
        aAA3_Inversiones_Inmobiliarias
        aAA31_Terrenos
        aAA32_Construcciones
        aAA4_Inversiones_Grupo
        aAA41_Instrumentos_de_Patrimonio
        aAA42_Creditos_a_Empresas
        aAA43_Valores_Representativos_de_Deuda
        aAA44_Derivados
        aAA45_Otros_Activos_Financieros
        aAA46_Otras_Inversiones
        aAA5_Inversiones_Financieras_LP
        aAA51_Instrumentos_de_Patrimonio
        aAA52_Creditos_a_terceros
        aAA53_Valores_Representativos_de_Deuda
        aAA54_Derivados
        aAA55_Otros_Activos_Financieros
        aAA56_Otras_Inversiones
        aAA6_Activos_por_impuesto_diferido
        aAA7_Deudas_Comerciales_no_corrientes
        aAB_Activo_Corriente
        aAB1_Activos_no_corrientes_a_la_venta
        aAB2_Existencias
        aAB21_Existencias_Comerciales
        aAB22_Materias_Primas
        aAB23_Productos_en_curso
        aAB24_Productos_terminados
        aAB25_Subproductos
        aAB26_Anticipos_a_Proveedores
        aAB3_Deudores_comerciales
        aAB31_Clientes
        aAB32_Clientes_Grupo
        aAB33_Deudores_varios
        aAB34_Personal
        aAB35_Activos_por_Impuesto_Corriente
        aAB36_Otros_creditos_Adm_Pub
        aAB37_Accionistas_por_desembolsos_exigidos
        aAB4_Inversiones_en_empresas_Grupo
        aAB41_Instrumentos_de_Patrimonio
        aAB42_Creditos_a_Empresas
        aAB43_Valores_Representativos_de_Deuda
        aAB44_Derivados
        aAB45_Otros_Activos_Financieros
        aAB46_Otras_Inversiones
        aAB5_Inversiones_financieras_a_corto
        aAB51_Instrumentos_de_Patrimonio
        aAB52_Creditos_a_Empresas
        aAB53_Valores_Representativos_de_Deuda
        aAB54_Derivados
        aAB55_Otros_Activos_Financieros
        aAB56_Otras_Inversiones
        aAB6_Periodificaciones_a_corto
        aAB7_Efectivo
        aAB71_Tesoreria
        aAB72_Otros_activos_liquidos
        aB_Pasivo
        aBA_Patrimonio_Neto
        aBA1_Fondos_Propios
        aBA11_Capital
        aBA111_Capital_Escriturado
        aBA112_Capital_no_exigido
        aBA12_Prima_de_emision
        aBA13_Reservas
        aBA131_Reservas_legales
        aBA132_Reservas_voluntarias
        aBA133_Reserva_de_Revalorizacion
        aBA14_Autocartera
        aBA15_Resultados_Ejercicios_anteriores
        aBA151_Remanente
        aBA152_Resultados_negativos_ejercicios_anteriores
        aBA16_Otras_aportaciones_de_socios
        aBA17_Resultado_del_ejercicio
        aBA18_Dividemdo_a_cuenta
        aBA19_Otros_instr_patr_neto
        aBA2_Ajustes_por_cambios_de_valor
        aBA21_Activos_financieros_a_la_venta
        aBA22_Operaciones_de_cobertura
        aBA23_Activos_no_corrientes
        aBA24_Diferencia_de_Conversion
        aBA25_Otros_ajustes
        aBA3_Subvenciones
        aBB_Pasivo_No_Corriente
        aBB1_Provisiones_LP
        aBB11_Obligaciones_personal
        aBB12_Medioambiental
        aBB13_Reestructuracion
        aBB14_Otras_provisiones
        aBB2_Deudas_LP
        aBB21_Obligaciones
        aBB22_Deudas_con_Entidades_de_credito
        aBB23_Leasing
        aBB24_Derivados
        aBB25_Otros_pasivos_financieros
        aBB3_Deudas_grupo
        aBB4_Pasivos_impuesto_diferido
        aBB5_Periodificaciones_LP
        aBB6_Acreedores_comerciales_no_corrientes
        aBB7_Deuda_especial_LP
        aBC_Pasivo_Corriente
        aBC1_Vinculados_a_act_no_corr_a_la_venta
        aBC2_Provisiones_CP
        aBC21_Gases_efto_invernadero
        aBC22_Otras_provisiones
        aBC3_Deudas_CP
        aBC31_Obligaciones
        aBC32_Deudas_CP_con_entidades_de_credito
        aBC33_Leasing
        aBC34_Derivados
        aBC35_Otros_pasivos_financieros
        aBC4_Deudas_Grupo_CP
        aBC5_Acreedores
        aBC51_Proveedores
        aBC52_Proveedores_grupo
        aBC53_Acreedores_varios
        aBC54_Personal
        aBC55_Pasivos_por_impuesto_corriente
        aBC56_Administraciones_Publicas
        aBC57_Anticipos_de_Clientes
        aBC6_Periodificaciones_CP
        aBC7_Deuda_especial_CP
        b_Cuenta_Explotacion
        bA_Operaciones_Continuadas
        bA1_Resultado_de_explotacion
        bA101_Turnover
        bA101A_Ventas
        bA101B_Servicios
        bA101C
        bA102_Variacion_Existencias
        bA103_Trabajos
        bA104_Aprovisionamientos
        bA104a
        bA104b
        bA104c
        bA104d
        bA105_Otros_ingresos_de_explotacion
        bA105a_Ingresos_accesorios
        bA105b_Subvenciones
        bA106_Personal
        bA106a_Sueldos
        bA106b_Cargas_sociales
        bA106c_Provisiones
        bA107_Otros_gastos_explotacion
        bA107a_Servicios_exteriores
        bA107b_Tributos
        bA107c_Deterioro
        bA107d_Otros
        bA107e_Gases
        bA108_Amortizaciones
        bA109_Subvenciones
        bA110_Exceso_de_Provisiones
        bA111_Deterioro
        bA111A
        bA111b
        bA111c
        bA112
        bA113
        bA2_Resultado_Financiero
        bA214_Ingresos_Financieros
        bA214a
        bA214b
        bA214c
        bA215_Gastos_Financieros
        bA215a
        bA215b
        bA215c
        bA216
        bA216a
        bA216b
        bA217_Diferencias_de_cambio
        bA218
        bA218a
        bA218b
        bA219
        bA219a
        bA219b
        bA219c
        bA3_Resultado_antes_de_impuestos
        bA320_Impuesto_sobre_beneficios
        bA4_Resultado_de_operaciones_Continuadas
        bB_Operaciones_Interrumpidas
        bB21_Resultado_OI_neto_de_Impuestos
        bA5_Resultado_del_ejercicio
    End Enum


    Public Enum Levels
        NotSet
        Uno
        Dos
        Tres
        Cuatro
        Cinco
        Seis
        Siete
    End Enum

    Public Sub New()
        MyBase.New()
        _Children = New List(Of DTOPgcClass)
        _Ctas = New List(Of DTOPgcCta)
        _Sumandos = New List(Of DTOPgcClass.Cods)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Children = New List(Of DTOPgcClass)
        _Ctas = New List(Of DTOPgcCta)
        _Sumandos = New List(Of DTOPgcClass.Cods)
    End Sub

    Shared Function Root(oClass As DTOPgcClass) As DTOPgcClass
        Dim retval As DTOPgcClass = Nothing
        Do Until oClass.Parent Is Nothing
            oClass = oClass.Parent
        Loop
        Return oClass
    End Function

    Public Function GetLevel() As Integer
        Dim retval = 0
        Dim oParent = _Parent
        Do While oParent IsNot Nothing
            retval += 1
            oParent = oParent.Parent
        Loop
        Return retval
    End Function

    Shared Function Eur(oClass As DTOPgcClass, year As Integer, mes As Integer) As Decimal
        Dim retval As Decimal

        If oClass.Ctas IsNot Nothing Then
            If oClass.YearMonths Is Nothing Then
                For Each oCta In oClass.Ctas
                    If oCta.YearMonths IsNot Nothing Then
                        Dim oYearMonth = oCta.YearMonths.FirstOrDefault(Function(x) x.Year = year And x.Month = mes)
                        If oYearMonth IsNot Nothing Then retval += oYearMonth.Eur
                    End If
                Next
            Else
                Dim oYearMonth = oClass.YearMonths.FirstOrDefault(Function(x) x.Year = year And x.Month = mes)
                If oYearMonth IsNot Nothing Then retval += oYearMonth.Eur
            End If
        End If

        For Each oChild In oClass.Children
            retval += Eur(oChild, year, mes)
        Next
        Return retval
    End Function

    Shared Function Eur(oClasses As List(Of DTOPgcClass), oClassCod As DTOPgcClass.Cods, year As Integer, mes As Integer) As Decimal
        Dim oClass As DTOPgcClass = DTOPgcClass.RecursiveSearch(oClasses, oClassCod)
        Dim retval As Decimal = Eur(oClass, year, mes)
        Return retval
    End Function


    Shared Function RecursiveSearch(oNodes As List(Of DTOPgcClass), oCod As DTOPgcClass.Cods) As DTOPgcClass
        Dim retval As DTOPgcClass = Nothing
        For Each oClass As DTOPgcClass In oNodes
            If oClass.Cod = oCod Then
                retval = oClass
                Exit For
            ElseIf oClass.Children.Count > 0 Then
                retval = RecursiveSearch(oClass.Children, oCod)
                If retval IsNot Nothing Then Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function RecursiveSearch(src As DTOPgcClass, oCod As DTOPgcClass.Cods) As DTOPgcClass
        Dim retval As DTOPgcClass = Nothing
        If src.Cod = oCod Then
            retval = src
        Else
            For Each oChild As DTOPgcClass In src.Children
                Dim tmp As DTOPgcClass = RecursiveSearch(oChild, oCod)
                If tmp IsNot Nothing Then
                    retval = tmp
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function Tree(ByVal src As List(Of DTOPgcClass)) As List(Of DTOPgcClass)
        Dim retval As New List(Of DTOPgcClass)
        For Each item In src
            'If item.Cod = 87 Then Stop
            If item.Parent Is Nothing Then
                retval.Add(item)
            Else
                Dim oParent As DTOPgcClass = src.Find(Function(x) x.Guid.Equals(item.Parent.Guid))
                If oParent IsNot Nothing Then
                    item.Parent = oParent
                    oParent.Children.Add(item)
                End If
            End If
        Next
        Return retval
    End Function

    Shared Function TreeSearch(oTree As List(Of DTOPgcClass), oCod As DTOPgcClass.Cods) As DTOPgcClass
        Dim retval = oTree.FirstOrDefault(Function(x) x.Cod = oCod)
        If retval Is Nothing Then
            For Each item In oTree
                If item.Children IsNot Nothing Then
                    retval = TreeSearch(item.Children, oCod)
                    If retval IsNot Nothing Then Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Shared Sub SetLevels(ByRef values As List(Of DTOPgcClass))
        For Each item As DTOPgcClass In values
            If item.Parent IsNot Nothing Then
                item.Level = item.Parent.Level + 1
            End If
            SetLevels(item.Children)
        Next
    End Sub

    Shared Sub CalcSumandos(ByRef values As List(Of DTOPgcClass))
        For Each item As DTOPgcClass In values
            If item.Sumandos.Count > 0 Then
                item.CurrentDeb = 0
                item.CurrentHab = 0
                item.PreviousDeb = 0
                item.PreviousHab = 0

                For Each oCod As DTOPgcClass.Cods In item.Sumandos
                    'Dim oSumando As DTOPgcClass = values.Find(Function(x) x.Cod.Equals(oCod))
                    Dim oSumando As DTOPgcClass = DTOPgcClass.RecursiveSearch(values, oCod)
                    If oSumando IsNot Nothing Then
                        With oSumando
                            item.CurrentDeb += .CurrentDeb
                            item.CurrentHab += .CurrentHab
                            item.PreviousDeb += .PreviousDeb
                            item.PreviousHab += .PreviousHab
                        End With
                    End If
                Next
            End If
        Next

    End Sub

    Shared Sub ReverseSignOnAssets(oActiu As DTOPgcClass)
        If oActiu.Ctas.Count = 0 Then
            For Each oChild In oActiu.Children
                ReverseSignOnAssets(oChild)
            Next
        Else
            For Each oCta In oActiu.Ctas
                For Each oYearMonth As DTOYearMonth In oCta.YearMonths
                    oYearMonth.Eur = -oYearMonth.Eur
                Next
            Next
        End If
    End Sub

    Shared Sub SetResultats(oTree As List(Of DTOPgcClass), FromYear As Integer)
        SetResultadoDelEjercicio(oTree, FromYear)
        SetResultadoAntesDeImpuestos(oTree, FromYear)
    End Sub

    Shared Sub SetResultadoDelEjercicio(oTree As List(Of DTOPgcClass), FromYear As Integer)
        Dim oActiu As DTOPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aA_Activo)
        Dim oPassiu As DTOPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aB_Pasivo)
        Dim oResultats As DTOPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.aBA17_Resultado_del_ejercicio)
        Dim oCta129 As DTOPgcCta = oResultats.Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.resultatsAnyCorrent)
        oCta129.YearMonths = New List(Of DTOYearMonth)
        For Year As Integer = FromYear To Today.Year
            For mes As Integer = 1 To 12
                Dim dcActiu As Decimal = DTOPgcClass.Eur(oActiu, Year, mes)
                Dim dcPassiu As Decimal = DTOPgcClass.Eur(oPassiu, Year, mes)
                Dim dcResultat = dcActiu - dcPassiu
                Dim oYearMonth = New DTOYearMonth(Year, mes, dcResultat)
                oCta129.YearMonths.Add(oYearMonth)
            Next
        Next

    End Sub

    Shared Sub SetResultadoDeExplotacion(oTree As List(Of DTOPgcClass), FromYear As Integer)
        Dim oPLBA101 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA101_Turnover)
        Dim oPLBA102 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA102_Variacion_Existencias)
        Dim oPLBA103 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA103_Trabajos)
        Dim oPLBA104 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA104_Aprovisionamientos)
        Dim oPLBA105 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA105_Otros_ingresos_de_explotacion)
        Dim oPLBA106 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA106_Personal)
        Dim oPLBA107 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA107_Otros_gastos_explotacion)
        Dim oPLBA108 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA108_Amortizaciones)
        Dim oPLBA109 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA109_Subvenciones)
        Dim oPLBA110 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA110_Exceso_de_Provisiones)
        Dim oPLBA111 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA111_Deterioro)
        Dim oPLBA112 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA112)
        Dim oPLBA113 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA113)

        Dim oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA1_Resultado_de_explotacion)
        oPgcClass.YearMonths = New List(Of DTOYearMonth)
        For Year As Integer = FromYear To Today.Year
            For mes As Integer = 1 To 12
                Dim dcPLBA101 As Decimal = DTOPgcClass.Eur(oPLBA101, Year, mes)
                Dim dcPLBA102 As Decimal = DTOPgcClass.Eur(oPLBA102, Year, mes)
                Dim dcPLBA103 As Decimal = DTOPgcClass.Eur(oPLBA103, Year, mes)
                Dim dcPLBA104 As Decimal = DTOPgcClass.Eur(oPLBA104, Year, mes)
                Dim dcPLBA105 As Decimal = DTOPgcClass.Eur(oPLBA105, Year, mes)
                Dim dcPLBA106 As Decimal = DTOPgcClass.Eur(oPLBA106, Year, mes)
                Dim dcPLBA107 As Decimal = DTOPgcClass.Eur(oPLBA107, Year, mes)
                Dim dcPLBA108 As Decimal = DTOPgcClass.Eur(oPLBA108, Year, mes)
                Dim dcPLBA109 As Decimal = DTOPgcClass.Eur(oPLBA109, Year, mes)
                Dim dcPLBA110 As Decimal = DTOPgcClass.Eur(oPLBA110, Year, mes)
                Dim dcPLBA111 As Decimal = DTOPgcClass.Eur(oPLBA111, Year, mes)
                Dim dcPLBA112 As Decimal = DTOPgcClass.Eur(oPLBA112, Year, mes)
                Dim dcPLBA113 As Decimal = DTOPgcClass.Eur(oPLBA113, Year, mes)
                Dim dcEur = dcPLBA101 + dcPLBA102 + dcPLBA103 + dcPLBA104 + dcPLBA105 + dcPLBA106 + dcPLBA107 + dcPLBA108 + dcPLBA109 + dcPLBA110 + dcPLBA111 + dcPLBA112 + dcPLBA113
                Dim oYearMonth = New DTOYearMonth(Year, mes, dcEur)
                oPgcClass.YearMonths.Add(oYearMonth)
                If Year = 2017 And mes = 1 Then Stop
            Next
        Next

    End Sub

    Shared Sub SetResultadoFinanciero(oTree As List(Of DTOPgcClass), FromYear As Integer)
        Dim oPLBA214 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA214_Ingresos_Financieros)
        Dim oPLBA215 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA215_Gastos_Financieros)
        Dim oPLBA216 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA216)
        Dim oPLBA217 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA217_Diferencias_de_cambio)
        Dim oPLBA218 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA218)
        Dim oPLBA219 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA219)

        Dim oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA2_Resultado_Financiero)
        oPgcClass.YearMonths = New List(Of DTOYearMonth)
        For Year As Integer = FromYear To Today.Year
            For mes As Integer = 1 To 12
                Dim dcPLBA214 As Decimal = DTOPgcClass.Eur(oPLBA214, Year, mes)
                Dim dcPLBA215 As Decimal = DTOPgcClass.Eur(oPLBA215, Year, mes)
                Dim dcPLBA216 As Decimal = DTOPgcClass.Eur(oPLBA216, Year, mes)
                Dim dcPLBA217 As Decimal = DTOPgcClass.Eur(oPLBA217, Year, mes)
                Dim dcPLBA218 As Decimal = DTOPgcClass.Eur(oPLBA218, Year, mes)
                Dim dcPLBA219 As Decimal = DTOPgcClass.Eur(oPLBA219, Year, mes)
                Dim dcEur = dcPLBA214 + dcPLBA215 + dcPLBA216 + dcPLBA217 + dcPLBA218 + dcPLBA219
                Dim oYearMonth = New DTOYearMonth(Year, mes, dcEur)
                oPgcClass.YearMonths.Add(oYearMonth)
            Next
        Next

    End Sub

    Shared Sub SetResultadoAntesDeImpuestos(oTree As List(Of DTOPgcClass), FromYear As Integer)
        Dim oPLBA1 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA1_Resultado_de_explotacion)
        Dim oPLBA2 = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA2_Resultado_Financiero)

        Dim oPgcClass = DTOPgcClass.TreeSearch(oTree, DTOPgcClass.Cods.bA3_Resultado_antes_de_impuestos)
        oPgcClass.YearMonths = New List(Of DTOYearMonth)
        For Year As Integer = FromYear To Today.Year
            For mes As Integer = 1 To 12
                Dim dcResultatExplotacio As Decimal = DTOPgcClass.Eur(oPLBA1, Year, mes)
                Dim dcResultatFinancer As Decimal = DTOPgcClass.Eur(oPLBA2, Year, mes)
                Dim dcEur = dcResultatExplotacio + dcResultatFinancer
                Dim oYearMonth = New DTOYearMonth(Year, mes, dcEur)
                oPgcClass.YearMonths.Add(oYearMonth)
            Next
        Next

    End Sub



End Class
