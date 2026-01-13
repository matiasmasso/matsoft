Public Class Frm_Intrastats

    Private _DefaultValue As DTOIntrastat
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOIntrastat = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Intrastats_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Intrastats1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Intrastats1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Intrastats1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Intrastats1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oIntrastats = Await FEB.Intrastats.All(Current.Session.Emp, exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Intrastats1.Load(oIntrastats, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub RequestToAddNewIn(sender As Object, e As EventArgs) Handles _
            Xl_Intrastats1.RequestToAddNewIn, DeIntroduccióToolStripMenuItem.Click
        Do_AddNew(DTOIntrastat.Flujos.Introduccion)
    End Sub

    Private Sub RequestToAddNewOut(sender As Object, e As EventArgs) Handles _
            Xl_Intrastats1.RequestToAddNewOut, DeExpedicióToolStripMenuItem.Click
        Do_AddNew(DTOIntrastat.Flujos.Expedicion)
    End Sub

    Private Sub Do_AddNew(oFluxe As DTOIntrastat.Flujos)
        Dim oPreviousYearMonth = DTOYearMonth.FromFch(DTO.GlobalVariables.Today().AddMonths(-1))
        Dim oFrm As New Frm_IntrastatFactory(oPreviousYearMonth, oFluxe, True)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()

    End Sub

End Class