Public Class Frm_LastCcas
    Private _DefaultValue As DTOCca
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _allowevents As Boolean

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOCca = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Ccas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await UIHelper.FadeIn(Me, 40)
        LoadYears()
        refresca()
        _allowevents = True
    End Sub



    Private Sub Xl_Ccas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Ccas1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Ccas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Ccas1.RequestToAddNew
        Dim oCca As New DTOCca
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Ccas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Ccas1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        ProgressBar1.Visible = True
        Dim oExercici As DTOExercici = CurrentExercici()
        Dim exs As New List(Of Exception)
        Dim oCcas = Await FEB2.Ccas.Headers(oExercici, exs)
        If exs.Count = 0 Then
            ProgressBar1.Visible = False
            Xl_Ccas1.Load(oCcas)
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function CurrentExercici() As DTOExercici
        Dim year As Integer = Xl_Years1.Value
        Dim retval As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, year)
        Return retval
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Ccas1.Filter = e.Argument
    End Sub

    Private Sub LoadYears()
        Dim years As New List(Of Integer)
        For i = 1985 To Today.Year
            years.Insert(0, i)
        Next
        Xl_Years1.Load(years, Today.Year)
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _allowevents Then
            refresca()
        End If
    End Sub

    Private Async Sub LlibreDiariToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LlibreDiariToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oExercici = CurrentExercici()
        Dim oStream As Byte() = Await FEB2.LlibreDiari.Excel(exs, oExercici, Current.Session.Lang)
        Dim sFilename As String = String.Format("{0}.{1} Llibre Diari.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        If UIHelper.ShowExcel(exs, oStream, sFilename) Then
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub LlibreMajorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LlibreMajorToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB2.LlibreMajor.Excel(exs, CurrentExercici)

        If UIHelper.ShowExcel(oSheet, exs) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        Xl_ProgressBar1.ShowProgress(min, max, value, label, CancelRequest)
    End Sub


End Class