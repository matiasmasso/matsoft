Public Class Frm_CustomerCluster
    Private _CustomerCluster As DTOCustomerCluster
    Private _ChildrenLoaded As Boolean
    Private _AllowEvents As Boolean


    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Children
    End Enum

    Public Sub New(value As DTOCustomerCluster)
        MyBase.New()
        Me.InitializeComponent()
        _CustomerCluster = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.CustomerCluster.Load(exs, _CustomerCluster) Then
            With _CustomerCluster
                Me.Text = "Clusters de " & .Holding.Nom
                TextBoxNom.Text = .Nom
                TextBoxObs.Text = .Obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxObs.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(PanelButtons, True)
        With _CustomerCluster
            .nom = TextBoxNom.Text
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.CustomerCluster.Update(exs, _CustomerCluster) Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerCluster))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.CustomerCluster.Delete(exs, _CustomerCluster) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerCluster))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Children
                If Not _ChildrenLoaded Then
                    Dim oCustomers = Await FEB2.CustomerCluster.Children(exs, _CustomerCluster)
                    If exs.Count = 0 Then
                        Xl_Contacts1.Load(oCustomers)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub
End Class


