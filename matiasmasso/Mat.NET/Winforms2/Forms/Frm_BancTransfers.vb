Public Class Frm_BancTransfers
    Private _Banc As DTOBanc

    Public Sub New(Optional oBanc As DTOBanc = Nothing)
        MyBase.New
        InitializeComponent()
        _Banc = oBanc
    End Sub

    Private Async Sub Frm_BancTransfers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
        SetMenu()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oBancTransferPools = Await FEB.BancTransferPools.All(exs, GlobalVariables.Emp, _Banc)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_BancTransfers1.Load(oBancTransferPools)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub SetMenu()
        ArxiuToolStripMenuItem.DropDownItems.Add("Nova transferencia de nómines al personal", Nothing, AddressOf Do_FactoryStaff)
        ArxiuToolStripMenuItem.DropDownItems.Add("Nova transferencia de comisions als representants", Nothing, AddressOf Do_FactoryReps)
        ArxiuToolStripMenuItem.DropDownItems.Add("Nova transferencia lliure", Nothing, AddressOf Do_FactoryAlt)
        ArxiuToolStripMenuItem.DropDownItems.Add("Nou traspas entre els nostres bancs", Nothing, AddressOf Do_Traspas)
    End Sub

    Private Sub Do_FactoryStaff()
        Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Staff)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
    Private Sub Do_FactoryReps()
        Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Reps)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
    Private Sub Do_FactoryAlt()
        Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.NotSet)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
    Private Sub Do_Traspas()
        Dim oFrm As New Frm_BancTraspas(_Banc)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_BancTransfers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BancTransfers1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub Xl_BancTransfers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BancTransfers1.RequestToAddNew
        Dim oMode As Frm_BancTransferFactory.Modes = e.Argument
        Select Case oMode
            Case Frm_BancTransferFactory.Modes.Traspas
                Do_Traspas()
            Case Else
                Dim oFrm As New Frm_BancTransferFactory(oMode)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
        End Select
    End Sub
End Class