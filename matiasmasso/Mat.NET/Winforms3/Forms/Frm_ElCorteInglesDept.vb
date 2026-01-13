Public Class Frm_ElCorteInglesDept
    Private _Value As DTO.Integracions.ElCorteIngles.Dept
    Private _Items As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
    Private _AllowEvents As Boolean

    Private _TabLoaded(10) As Boolean

    Public Enum Tabs
        General
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTO.Integracions.ElCorteIngles.Dept)
        MyBase.New()
        Me.InitializeComponent()
        _Value = value
    End Sub

    Private Sub Frm_ElCorteInglesDept_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ElCorteInglesDept.Load(exs, _Value) Then
            With _Value
                Me.Text = String.Format("El Corte Ingles Dept.{0}", .Id)
                TextBoxId.Text = .Id
                TextBoxNom.Text = .Nom
                TextBoxTel.Text = .Tel
                Xl_LookupUserManager.Load(.Manager)
                Xl_LookupUserAssistant.Load(.Assistant)
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
        TextBoxId.TextChanged,
         TextBoxNom.TextChanged,
          TextBoxTel.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Value
            .Id = TextBoxId.Text
            .Nom = TextBoxNom.Text
            .Tel = TextBoxTel.Text
            .Manager = Xl_LookupUserManager.User
            .Assistant = Xl_LookupUserAssistant.User
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.ElCorteInglesDept.Upload(exs, _Value) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.ElCorteInglesDept.Delete(exs, _Value) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupUserManager_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupUserManager.AfterUpdate
        RefrescaManager(sender, e)
    End Sub

    Private Sub Xl_LookupUserAssistant_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupUserAssistant.AfterUpdate
        RefrescaAssistant(sender, e)
    End Sub

    Private Sub Xl_LookupUserManager_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupUserManager.RequestToLookup
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
        Dim oFrm As New Frm_Contact_Users(oCustomer, Xl_LookupUserManager.User, Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf RefrescaManager
        oFrm.Show()
    End Sub


    Private Sub Xl_LookupUserAssistant_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupUserAssistant.RequestToLookup
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
        Dim oFrm As New Frm_Contact_Users(oCustomer, Xl_LookupUserAssistant.User, Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf RefrescaAssistant
        oFrm.Show()
    End Sub

    Private Sub RefrescaManager(sender As Object, e As MatEventArgs)
        Dim value As DTOUser = e.Argument
        Xl_LookupUserManager.Load(value)
        ButtonOk.Enabled = True
    End Sub

    Private Sub RefrescaAssistant(sender As Object, e As MatEventArgs)
        Dim value As DTOUser = e.Argument
        Xl_LookupUserAssistant.Load(value)
        ButtonOk.Enabled = True
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim oTab = TabControl1.SelectedIndex
        If Not _TabLoaded(oTab) Then
            Select Case oTab
                Case Tabs.General
            End Select
            _TabLoaded(oTab) = True
        End If
    End Sub


    Private Sub Xl_TextboxSearchCataleg_AfterUpdate(sender As Object, e As MatEventArgs)
        'Xl_ElCorteInglesAlineamientoDisponibilidad1.Filter = e.Argument
    End Sub


End Class


