

Public Class Frm_Holding
    Private _Holding As DTOHolding
    Private _TabDone(10) As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Clusters
        Pncs
    End Enum

    Public Sub New(oHolding As DTOHolding)
        MyBase.New
        InitializeComponent()

        _Holding = oHolding
    End Sub

    Private Sub Frm_Holding_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Application.DoEvents()

        UIHelper.ToggleProggressBar(Panel1, True)
        If FEB.Holding.Load(exs, _Holding) Then
            refresca()
            UIHelper.ToggleProggressBar(Panel1, False)
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        With _Holding
            TextBoxNom.Text = String.Format("Holding: {0}", .nom)
            Xl_Contacts_Insertable1.Load(.Companies)
            Xl_ClustersTree1.Load(_Holding)
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles TextBoxNom.TextChanged,
        Xl_Contacts_Insertable1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_ControlRemoved(sender As Object, e As ControlEventArgs) Handles ButtonCancel.ControlRemoved
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Holding
            .nom = TextBoxNom.Text
            .Companies = Xl_Contacts_Insertable1.Contacts
        End With
        If Await FEB.Holding.Update(exs, _Holding) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Holding))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc = MsgBox("eliminem aquest holding?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Holding.Delete(exs, _Holding) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Holding))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub Xl_ClustersTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ClustersTree1.RequestToRefresh
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        _Holding = Await FEB.Holding.Find(exs, _Holding.Guid) '=========================== passar a FEB.Holding.Clusters
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            With _Holding
                TextBoxNom.Text = String.Format("Holding: {0}", .nom)
                Xl_Contacts_Insertable1.Load(.Companies)
                Xl_ClustersTree1.Load(_Holding)
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Pncs
                If Not _TabDone(Tabs.Pncs) Then
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oPncs = Await FEB.PurchaseOrderItems.Pending(exs, _Holding, DTOPurchaseOrder.Codis.client, GlobalVariables.Emp.Mgz, DTOCustomer.GroupLevels.Holding)
                    UIHelper.ToggleProggressBar(Panel1, False)
                    If exs.Count = 0 Then
                        Xl_CcxPncs1.Load(oPncs)
                    Else
                        UIHelper.WarnError(exs)
                    End If

                    _TabDone(Tabs.Pncs) = True
                End If
        End Select
    End Sub
End Class