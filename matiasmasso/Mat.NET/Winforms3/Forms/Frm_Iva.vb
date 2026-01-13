Public Class Frm_Iva
    Private _IvaLiquidacio As DTOIVALiquidacio
    Private _AllCtas As List(Of DTOPgcCta)
    Private _AllowEvents As Boolean


    Private Async Sub Frm_Iva_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _AllCtas = Await FEB.PgcCtas.All(exs)
        If exs.Count = 0 Then
            Dim DefaultFch As Date = DTO.GlobalVariables.Today().AddMonths(-1)
            LoadYears(DefaultFch)
            LoadMonths(DefaultFch)
            Await refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function refresca() As Task
        Dim oExercici = CurrentExercici()
        Dim oYearMonth = CurrentYearMonth()

        Xl_Years1.Visible = False
        Xl_BaseQuotas1.Visible = False
        ComboBoxMonth.Visible = False
        ProgressBar1.Visible = True
        Application.DoEvents()

        Dim exs As New List(Of Exception)
        _IvaLiquidacio = Await FEB.IvaLiquidacio.Factory(exs, oExercici, oYearMonth.Month)
        ProgressBar1.Visible = False
        Application.DoEvents()

        If exs.Count = 0 Then
            Xl_BaseQuotas1.Load(_IvaLiquidacio, _AllCtas)
            VeureAssentamentLiquidacióToolStripMenuItem.Enabled = _IvaLiquidacio.Cca IsNot Nothing
            GeneraAssentamentLiquidacióToolStripMenuItem.Enabled = _IvaLiquidacio.Cca Is Nothing
            Xl_Years1.Visible = True
            Xl_BaseQuotas1.Visible = True
            ComboBoxMonth.Visible = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentExercici() As DTOExercici
        Return DTOExercici.FromYear(Current.Session.Emp, Xl_Years1.Value)
    End Function

    Private Function CurrentYearMonth() As DTOYearMonth
        Dim oExercici = CurrentExercici()
        Dim iMonth As Integer = ComboBoxMonth.SelectedIndex + 1
        Dim retval = New DTOYearMonth(oExercici.Year, iMonth)
        Return retval
    End Function

    Private Sub LoadYears(defaultfch As Date)
        Dim iyears As New List(Of Integer)
        For i As Integer = DTO.GlobalVariables.Today().Year To 2012 Step -1
            iyears.Add(i)
        Next
        Xl_Years1.Load(iyears, defaultfch.Year)
    End Sub

    Private Sub LoadMonths(defaultfch As Date)
        Dim oLang As DTOLang = Current.Session.Lang
        Dim months As New List(Of String)
        For i As Integer = 1 To 12
            months.Add(oLang.MesAbr(i))
        Next
        ComboBoxMonth.DataSource = months
        ComboBoxMonth.SelectedIndex = defaultfch.Month - 1
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oExcel As MatHelper.Excel.Sheet = Xl_BaseQuotas1.ExcelSheet
        oExcel.Name = "IVA Mod.303"

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oExcel, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_AfterUpdate(sender As Object, e As MatEventArgs) Handles _
        Xl_Years1.AfterUpdate
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Sub ComboBoxMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMonth.SelectedIndexChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Sub VeureAssentamentLiquidacióToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VeureAssentamentLiquidacióToolStripMenuItem.Click
        Dim oFrm As New Frm_Cca(_IvaLiquidacio.Cca)
        oFrm.Show()
    End Sub

    Private Sub GeneraAssentamentLiquidacióToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeneraAssentamentLiquidacióToolStripMenuItem.Click
        Dim oCca = _IvaLiquidacio.CcaFactory(Current.Session.User, _AllCtas)
        Dim oFrm As New Frm_Cca(oCca)
        oFrm.Show()
    End Sub
End Class