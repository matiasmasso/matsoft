Public Class Balance
    Inherits _FeblBase

    Shared Async Function Tree(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of DTOBalance)
        Dim oYearMonthTo = DTOYearMonth.current
        Dim retval = Await Api.Fetch(Of DTOBalance)(exs, "balance/tree", oEmp.Id, year)
        For Each oParent In retval.items
            restoreParents(oParent, retval)
        Next

        Return retval
    End Function

    Shared Sub restoreParents(ByRef oParent As DTOPgcClass, ByRef oBalance As DTOBalance)
        'If oParent.Nom.esp = "1. Proveedores" Then Stop
        oParent.YearMonths = oBalance.YearMonths(oParent)
        If oParent.Children IsNot Nothing Then
            For Each oChild In oParent.Children
                oChild.Parent = oParent
                restoreParents(oChild, oBalance)
            Next
        End If
    End Sub

    Shared Async Function Tree(oEmp As DTOEmp, Optional DtFchTo As Date = Nothing) As Task(Of List(Of DTOPgcClass))
        Dim exs As New List(Of Exception)
        If DtFchTo = Nothing Then DtFchTo = DTO.GlobalVariables.Today()
        Dim oPlan = PgcPlan.FromYearSync(DtFchTo.Year, exs)
        Dim AllClasses = Await PgcClasses.All(exs, oPlan)
        Dim retval As List(Of DTOPgcClass) = DTOPgcClass.Tree(AllClasses)

        Dim oExercici As DTOExercici = DTOExercici.FromYear(oEmp, DtFchTo.Year)
        Dim oSumasYSaldos As List(Of DTOBalanceSaldo) = Balance.SumasySaldosSync(exs, oExercici, DtFchTo)

        For Each oClass As DTOPgcClass In retval
            Balance.Merge(oClass, oSumasYSaldos)
        Next

        DTOPgcClass.SetLevels(retval)
        DTOPgcClass.CalcSumandos(AllClasses)

        Dim oResultados As DTOPgcClass = retval.Find(Function(x) x.Cod = DTOPgcClass.Cods.b_Cuenta_Explotacion)
        Dim oPassiu As DTOPgcClass = retval.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        If oPassiu IsNot Nothing AndAlso oResultados IsNot Nothing Then
            Balance.InsertResultadosIntoPassiu(oPassiu, oResultados)
        End If

        Return retval
    End Function

    Shared Async Function Ratios(oEmp As DTOEmp, Optional DtFchTo As Date = Nothing) As Task(Of List(Of DTORatio))
        Dim oTree As List(Of DTOPgcClass) = Await Tree(oEmp, DtFchTo)
        Dim retval As New List(Of DTORatio)
        retval.Add(Balance.FondoDeManiobra(oTree))
        retval.Add(Balance.RatioDeLiquidez(oTree))
        retval.Add(Balance.RatioDeTesoreria(oTree))
        retval.Add(Balance.RatioDeDisponibilidad(oTree))
        retval.Add(Balance.RatioDeEndeudamiento(oTree))
        retval.Add(Balance.RatioCalidadDeuda(oTree))
        Return retval
    End Function


    Shared Async Function Excel(oEmp As DTOEmp, Optional DtFchTo As Date = Nothing) As Task(Of MatHelper.Excel.Book)
        Dim oLang As DTOLang = DTOApp.Current.Lang
        Dim oTree As List(Of DTOPgcClass) = Await Tree(oEmp, DtFchTo)
        Dim retval As New MatHelper.Excel.Book(oEmp.Org.PrimaryNifValue() & "." & DtFchTo.Year & ".Balanç i Compte resultats.xlsx")

        Dim oResultados As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.b_Cuenta_Explotacion)
        Dim oSheetResultados As New MatHelper.Excel.Sheet
        retval.Sheets.Add(oSheetResultados)
        oSheetResultados.Name = oResultados.Nom.Tradueix(oLang)
        Balance.FormatBalanceSheet(oEmp, oSheetResultados, DtFchTo)

        Dim oPassiu As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        Dim oSheetPassiu As New MatHelper.Excel.Sheet
        retval.Sheets.Add(oSheetPassiu)
        Balance.FormatBalanceSheet(oEmp, oSheetPassiu, DtFchTo)
        oSheetPassiu.Name = oPassiu.Nom.Tradueix(oLang)

        Dim oActiu As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aA_Activo)
        Dim oSheetActiu As New MatHelper.Excel.Sheet
        retval.Sheets.Add(oSheetActiu)
        Balance.FormatBalanceSheet(oEmp, oSheetActiu, DtFchTo)
        oSheetActiu.Name = oActiu.Nom.Tradueix(oLang)

        Balance.AddClass(oSheetActiu, oActiu.Cod, oActiu, oLang)
        Balance.AddClass(oSheetPassiu, oPassiu.Cod, oPassiu, oLang)
        Balance.AddClass(oSheetResultados, oResultados.Cod, oResultados, oLang)

        Return retval
    End Function



    Shared Async Function SumasySaldos(exs As List(Of Exception), oExercici As DTOExercici, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOBalanceSaldo))
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Await Api.Fetch(Of List(Of DTOBalanceSaldo))(exs, "Balance/sumasysaldos", oExercici.Emp.Id, oExercici.Year, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function SumasySaldosSync(exs As List(Of Exception), oExercici As DTOExercici, Optional DtFch As Date = Nothing) As List(Of DTOBalanceSaldo)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Api.FetchSync(Of List(Of DTOBalanceSaldo))(exs, "Balance/sumasysaldos", oExercici.Emp.Id, oExercici.Year, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function CceSync(exs As List(Of Exception), oEmp As DTOEmp, Optional oCta As DTOPgcCta = Nothing, Optional DtFch As Date = Nothing) As List(Of DTOBalanceSaldo)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Api.FetchSync(Of List(Of DTOBalanceSaldo))(exs, "Balance/cce", oEmp.Id, OpcionalGuid(oCta), DtFch.ToString("yyyy-MM-dd"))
    End Function



    Shared Function Url(DtFch As Date, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Factory(AbsoluteUrl, "balances", DtFch.ToOADate)
        Return retval
    End Function

    Shared Sub Merge(oClass As DTOPgcClass, oCtas As List(Of DTOBalanceSaldo))
        If oClass.Children.Count = 0 Then
            Dim oClassCtas As List(Of DTOBalanceSaldo) = oCtas.FindAll(Function(x) x.PgcClass.Equals(oClass))
            If oClassCtas.Count > 0 Then
                With oClass
                    .Ctas.AddRange(oClassCtas)
                    .CurrentDeb = oClassCtas.Sum(Function(x) x.CurrentDeb)
                    .CurrentHab = oClassCtas.Sum(Function(x) x.CurrentHab)
                    .PreviousDeb = oClassCtas.Sum(Function(x) x.PreviousDeb)
                    .PreviousHab = oClassCtas.Sum(Function(x) x.PreviousHab)
                End With
                Propagate(oClass, oClass.CurrentDeb, oClass.CurrentHab, oClass.PreviousDeb, oClass.PreviousHab)
            End If
        Else
            For Each item As DTOPgcClass In oClass.Children
                Merge(item, oCtas)
            Next
        End If
    End Sub

    Shared Sub Propagate(oClass As DTOPgcClass, CurrentDeb As Decimal, CurrentHab As Decimal, PreviousDeb As Decimal, PreviousHab As Decimal)
        Dim oParent As DTOPgcClass = oClass.Parent
        If oParent IsNot Nothing Then
            With oParent
                .CurrentDeb += CurrentDeb
                .CurrentHab += CurrentHab
                .PreviousDeb += PreviousDeb
                .PreviousHab += PreviousHab
            End With
            Propagate(oParent, CurrentDeb, CurrentHab, PreviousDeb, PreviousHab)
        End If
    End Sub

    Shared Sub InsertResultadosIntoPassiu(oPassiu As DTOPgcClass, oResultados As DTOPgcClass)
        Dim oResultado = DTOPgcClass.RecursiveSearch(oPassiu, DTOPgcClass.Cods.aBA17_Resultado_del_ejercicio)
        If oResultado IsNot Nothing Then
            With oResultado
                .CurrentDeb = oResultados.CurrentDeb
                .CurrentHab = oResultados.CurrentHab
                .PreviousDeb = oResultados.PreviousDeb
                .PreviousHab = oResultados.PreviousHab
            End With
            Balance.Propagate(oResultado, oResultado.CurrentDeb, oResultado.CurrentHab, oResultado.PreviousDeb, oResultado.PreviousHab)
        End If
    End Sub

    Shared Function CurrentYearEur(value As DTOPgcClass, oCod As DTOPgcClass.Cods) As Decimal
        Dim retval As Decimal = 0
        Select Case oCod
            Case DTOPgcClass.Cods.aA_Activo
                retval = value.CurrentDeb - value.CurrentHab
            Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                retval = value.CurrentHab - value.CurrentDeb
        End Select
        Return retval
    End Function

    Shared Function CurrentYearEur(value As DTOBalanceSaldo, oCod As DTOPgcClass.Cods) As Decimal
        Dim retval As Decimal = 0
        Select Case oCod
            Case DTOPgcClass.Cods.aA_Activo
                retval = value.CurrentDeb - value.CurrentHab
            Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                retval = value.CurrentHab - value.CurrentDeb
        End Select
        Return retval
    End Function

    Shared Function PreviousYearEur(value As DTOBalanceSaldo, oCod As DTOPgcClass.Cods) As Decimal
        Dim retval As Decimal = 0
        Select Case oCod
            Case DTOPgcClass.Cods.aA_Activo ' DTOPgcClass.Cods.Activo
                retval = value.PreviousDeb - value.PreviousHab
            Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion ' DTOPgcClass.Cods.Pasivo, DTOPgcClass.Cods.PG
                retval = value.PreviousHab - value.PreviousDeb
        End Select
        Return retval
    End Function


    Shared Function PreviousYearEur(value As DTOPgcClass, oCod As DTOPgcClass.Cods) As Decimal
        Dim retval As Decimal = 0
        Select Case oCod
            Case DTOPgcClass.Cods.aA_Activo
                retval = value.PreviousDeb - value.PreviousHab
            Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                retval = value.PreviousHab - value.PreviousDeb
        End Select
        Return retval
    End Function


#Region "Ratios"

    Shared Function FondoDeManiobra(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Fondo de Maniobra"
            .Dsc = "Activo corriente - Pasivo corriente"
            .Value = ActivoCorriente(oTree) - PasivoCorriente(oTree)
            .Formato = DTORatio.Formatos.Eur
        End With
        Return retval
    End Function

    Shared Function RatioDeLiquidez(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Ratio de Liquidez"
            .Dsc = "Activo corriente / Pasivo corriente"
            .Value = ActivoCorriente(oTree) / PasivoCorriente(oTree)
            .Formato = DTORatio.Formatos.Ratio
        End With
        Return retval
    End Function

    Shared Function RatioDeTesoreria(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Ratio de Tesorería"
            .Dsc = "(Clientes + Disponible) / Pasivo corriente"
            .Value = (Clientes(oTree) + ActivoDisponible(oTree)) / PasivoCorriente(oTree)
            .Formato = DTORatio.Formatos.Ratio
        End With
        Return retval
    End Function

    Shared Function RatioDeDisponibilidad(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Ratio de Disponibilidad"
            .Dsc = "Disponible / Pasivo corriente"
            .Value = ActivoDisponible(oTree) / PasivoCorriente(oTree)
            .Formato = DTORatio.Formatos.Ratio
        End With
        Return retval
    End Function

    Shared Function RatioDeEndeudamiento(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Ratio de Endeudamiento"
            .Dsc = "(Deuda LP + Deuda CP) / Total Pasivo"
            .Value = (PasivoNoCorriente(oTree) + PasivoCorriente(oTree)) / TotalPasivo(oTree)
            .Formato = DTORatio.Formatos.Ratio
        End With
        Return retval
    End Function

    Shared Function RatioCalidadDeuda(oTree As List(Of DTOPgcClass)) As DTORatio
        Dim retval As New DTORatio
        With retval
            .Nom = "Ratio Calidad de la deuda"
            .Dsc = "Deuda CP / (Deuda LP + Deuda CP)"
            .Value = PasivoCorriente(oTree) / (PasivoNoCorriente(oTree) + PasivoCorriente(oTree))
            .Formato = DTORatio.Formatos.Ratio
        End With
        Return retval
    End Function

    Shared Function ActivoCorriente(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oActivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aA_Activo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oActivo, DTOPgcClass.Cods.aAB_Activo_Corriente)
        Dim retval As Decimal = oClass.CurrentDeb() - oClass.CurrentHab
        Return retval
    End Function

    Shared Function ActivoDisponible(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oActivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aA_Activo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oActivo, DTOPgcClass.Cods.aAB7_Efectivo)
        Dim retval As Decimal = oClass.CurrentDeb() - oClass.CurrentHab
        Return retval
    End Function

    Shared Function Clientes(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oActivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aA_Activo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oActivo, DTOPgcClass.Cods.aAB31_Clientes)
        Dim retval As Decimal = oClass.CurrentDeb() - oClass.CurrentHab
        Return retval
    End Function

    Shared Function TotalPasivo(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oClass As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        Dim retval As Decimal = oClass.CurrentHab() - oClass.CurrentDeb
        Return retval
    End Function

    Shared Function PatrimonioNeto(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oPasivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oPasivo, DTOPgcClass.Cods.aBA_Patrimonio_Neto)
        Dim retval As Decimal = oClass.CurrentHab() - oClass.CurrentDeb
        Return retval
    End Function

    Shared Function PasivoNoCorriente(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oPasivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oPasivo, DTOPgcClass.Cods.aBB_Pasivo_No_Corriente)
        Dim retval As Decimal = oClass.CurrentHab() - oClass.CurrentDeb
        Return retval
    End Function
    Shared Function PasivoCorriente(oTree As List(Of DTOPgcClass)) As Decimal
        Dim oPasivo As DTOPgcClass = oTree.Find(Function(x) x.Cod = DTOPgcClass.Cods.aB_Pasivo)
        Dim oClass = DTOPgcClass.RecursiveSearch(oPasivo, DTOPgcClass.Cods.aBC_Pasivo_Corriente)
        Dim retval As Decimal = oClass.CurrentHab() - oClass.CurrentDeb
        Return retval
    End Function
#End Region

#Region "Excel"
    Shared Sub FormatBalanceSheet(oEmp As DTOEmp, ByRef oSheet As MatHelper.Excel.Sheet, DtFchTo As Date)
        If DtFchTo = Nothing Then DtFchTo = DTO.GlobalVariables.Today()
        Dim oExercici As DTOExercici = DTOExercici.FromYear(oEmp, DtFchTo.Year)

        Dim sHeaderCurrent As String
        Dim sHeaderPrevious As String
        If DtFchTo = oExercici.LastFch Then
            sHeaderCurrent = oExercici.Year
            sHeaderPrevious = oExercici.Previous.Year
        Else
            sHeaderCurrent = DtFchTo.ToShortDateString
            sHeaderPrevious = DtFchTo.AddYears(-1).ToShortDateString
        End If

        With oSheet
            .AddColumn("Compte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(sHeaderCurrent, MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(sHeaderPrevious, MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
    End Sub
    Shared Sub AddClass(ByRef oSheet As MatHelper.Excel.Sheet, oSheetCod As DTOPgcClass.Cods, oClass As DTOPgcClass, oLang As DTOLang)
        Dim oRow As MatHelper.Excel.Row = oSheet.AddRow()

        Dim sb As New System.Text.StringBuilder
        sb.Append(New String(" ", 4 * oClass.Level))
        sb.Append(oClass.Nom.Tradueix(oLang))
        oRow.AddCell(sb.ToString())

        If Not oClass.HideFigures Then
            Select Case oSheetCod
                Case DTOPgcClass.Cods.aA_Activo
                    oRow.AddCell(oClass.CurrentDeb - oClass.CurrentHab)
                    oRow.AddCell(oClass.PreviousDeb - oClass.PreviousHab)
                Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                    oRow.AddCell(oClass.CurrentHab - oClass.CurrentDeb)
                    oRow.AddCell(oClass.PreviousHab - oClass.PreviousDeb)
            End Select
        End If

        For Each oCtaSaldo As DTOBalanceSaldo In oClass.Ctas
            oRow = oSheet.AddRow()
            sb = New System.Text.StringBuilder
            sb.Append(New String(" ", 4 * 6))
            sb.Append(DTOPgcCta.FullNom(oCtaSaldo, oLang))
            oRow.AddCell(sb.ToString())

            Select Case oSheetCod
                Case DTOPgcClass.Cods.aA_Activo
                    oRow.AddCell(oCtaSaldo.CurrentDeb - oCtaSaldo.CurrentHab)
                    oRow.AddCell(oCtaSaldo.PreviousDeb - oCtaSaldo.PreviousHab)
                Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                    oRow.AddCell(oCtaSaldo.CurrentHab - oCtaSaldo.CurrentDeb)
                    oRow.AddCell(oCtaSaldo.PreviousHab - oCtaSaldo.PreviousDeb)
            End Select

        Next

        For Each oChild As DTOPgcClass In oClass.Children
            AddClass(oSheet, oSheetCod, oChild, oLang)
        Next

    End Sub
#End Region

End Class
