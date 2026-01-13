Public Class Frm_Transmisions

    Private _DefaultValue As DTOTransmisio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOTransmisio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        MyBase.Opacity = 0
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Transmisions_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
        Await UIHelper.FadeIn(Me)
    End Sub

    Private Sub Xl_Transmisions1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Transmisions1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Transmisions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Transmisions1.RequestToAddNew
        Dim oFrm As New Frm_TransmisioNew
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Transmisions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Transmisions1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oTransmisions = Await FEB2.Transmisions.All(GlobalVariables.Emp.Mgz, exs)
        If exs.Count = 0 Then
            Xl_Transmisions1.Load(oTransmisions, _DefaultValue, _SelectionMode)
            SetMenu()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AddNew()
        Dim oFrm As New Frm_TransmisioNew
        AddHandler oFrm.afterupdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Transmisions1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Transmisions1.ValueChanged
        SetMenu
    End Sub

    Private Sub SetMenu()
        Dim oTransmisions As List(Of DTOTransmisio) = Xl_Transmisions1.SelectedValues
        ArxiuToolStripMenuItem.DropDownItems.Clear()

        If oTransmisions.Count > 0 Then
            Dim oMenu_Transmisio As New Menu_Transmisio(oTransmisions)
            AddHandler oMenu_Transmisio.AfterUpdate, AddressOf refresca
            ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenu_Transmisio.Range)
            ArxiuToolStripMenuItem.DropDownItems.Add("-")
        End If
        ArxiuToolStripMenuItem.DropDownItems.Add("Nova transmisio", Nothing, AddressOf Do_AddNew)
        ArxiuToolStripMenuItem.DropDownItems.Add("refresca", Nothing, AddressOf refresca)

    End Sub

    Private Sub Xl_Transmisions1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_Transmisions1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub
End Class