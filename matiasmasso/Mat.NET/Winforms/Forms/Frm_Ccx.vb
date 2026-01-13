Public Class Frm_Ccx
    Private _Ccx As DTOCustomer
    Private _Tab As Tabs
    Private _Control As Control
    Public Enum Tabs
        Centres
        Pncs
        Sellout
    End Enum

    Public Sub New(oCcx As DTOContact, Optional oTab As Tabs = Tabs.Centres)
        InitializeComponent()
        _Ccx = oCcx
        _Tab = oTab
    End Sub

    Private Async Sub Frm_Ccx_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB2.Contact.Load(_Ccx, exs)
        _Ccx = FEB2.Customer.CcxOrMe(exs, _Ccx)
        Me.Text = "Grup " & _Ccx.FullNom
        Await Xl_Contact_Logo1.Load(exs, _Ccx)
        refresca()
    End Sub



    Private Sub refresca()
        With SplitContainer1.Panel2.Controls
            .Clear()
        End With

        Select Case _Tab
            Case Tabs.Centres
                _Control = New Xl_CustomerCentres()
                AddHandler DirectCast(_Control, Xl_CustomerCentres).RequestToRefresh, AddressOf RefrescaControl
            Case Tabs.Pncs
                _Control = New Xl_CcxPncs()
                AddHandler DirectCast(_Control, Xl_CcxPncs).RequestToRefresh, AddressOf RefrescaControl
            Case Tabs.Sellout
                _Control = New Xl_SellOut()
                AddHandler DirectCast(_Control, Xl_SellOut).RequestToRefresh, AddressOf RefrescaControl
        End Select

        RefrescaControl(_Control, EventArgs.Empty)
        _Control.Dock = DockStyle.Fill

        With SplitContainer1.Panel2.Controls
            .Add(_Control)
        End With
    End Sub


    Private Async Sub RefrescaControl(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        If TypeOf sender Is Xl_CustomerCentres Then
            Dim oCentres = Await FEB2.Customer.Children(exs, _Ccx)
            If exs.Count = 0 Then
                DirectCast(sender, Xl_CustomerCentres).Load(oCentres)
            Else
                UIHelper.WarnError(exs)
            End If
        ElseIf TypeOf _Control Is Xl_CcxPncs Then
            Dim oPncs = Await FEB2.PurchaseOrderItems.Pending(exs, _Ccx, DTOPurchaseOrder.Codis.client, GlobalVariables.Emp.Mgz, DTOCustomer.GroupLevels.Chain)
            If exs.Count = 0 Then
                DirectCast(sender, Xl_CcxPncs).Load(oPncs)
            Else
                UIHelper.WarnError(exs)
            End If
        ElseIf TypeOf sender Is Xl_SellOut Then
            Dim oSellOut = Await FEB2.SellOut.Factory(exs, Current.Session.User)
            If exs.Count = 0 Then
                FEB2.SellOut.SetCcx(oSellOut, _Ccx)
                Dim pSellOut = Await FEB2.SellOut.Load(exs, oSellOut)
                If exs.Count = 0 Then
                    oSellOut = pSellOut
                    DirectCast(sender, Xl_SellOut).Load(oSellOut)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ToolStripButtonCentres_Click(sender As Object, e As EventArgs) Handles ToolStripButtonCentres.Click
        _Tab = Tabs.Centres
        refresca()
    End Sub

    Private Sub ToolStripButtonPncs_Click(sender As Object, e As EventArgs) Handles ToolStripButtonPncs.Click
        _Tab = Tabs.Pncs
        refresca()
    End Sub

    Private Sub ToolStripButtonSellOut_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSellOut.Click
        _Tab = Tabs.Sellout
        refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Select Case _Tab
            Case Tabs.Centres
                DirectCast(_Control, Xl_CustomerCentres).Filter = e.Argument
            Case Tabs.Pncs
                DirectCast(_Control, Xl_CcxPncs).Filter = e.Argument
        End Select
    End Sub
End Class