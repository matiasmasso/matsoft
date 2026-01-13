Public Class Frm_Location
    Private _Location As DTOLocation
    Private _BlDoneContacts As Boolean
    Private _BlDoneAlbarans As Boolean
    Private _BlDoneBankBranches As Boolean
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        Contacts
        Albarans
        BankBranches
    End Enum

    Public Sub New(ByVal oLocation As DTOLocation)
        MyBase.New()
        Me.InitializeComponent()
        _Location = oLocation
    End Sub

    Private Async Sub Frm_Location_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Location.IsNew Then
            FEB2.Zona.Load(_Location.Zona, exs)
        Else
            If Not FEB2.Location.Load(_Location, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        With _Location
            Xl_LookupArea1.Load(_Location.Zona)
            If .Zona.SplitByComarcas Then
                LabelComarca.Visible = True
                Xl_Comarcas1.Visible = True
                Dim oComarcas = Await FEB2.Comarcas.All(.Zona, exs)
                If exs.Count = 0 Then
                    Xl_Comarcas1.Load(oComarcas, True)
                    If .Comarca IsNot Nothing Then
                        Xl_Comarcas1.SelectedItem = oComarcas.FirstOrDefault(Function(x) x.Equals(.Comarca))
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
            TextBoxLocation.Text = .Nom

            If Not .IsNew Then
                ButtonDel.Enabled = True
            End If
        End With
        _AllowEvents = True

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxLocation.TextChanged,
         Xl_LookupArea1.AfterUpdate,
          Xl_Comarcas1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Location
            .nom = TextBoxLocation.Text
            .Zona = Xl_LookupArea1.Area
            .comarca = Xl_Comarcas1.Comarca
        End With

        If _Location.Zona.SplitByComarcas And _Location.Comarca Is Nothing Then
            MsgBox("Cal sel.leccionar la comarca", MsgBoxStyle.Exclamation)
        Else
            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.Location.Update(_Location, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Location))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.Location.Delete(exs, _Location) Then
            RaiseEvent AfterDelete(Me, New MatEventArgs(_Location))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Contacts
                If Not _BlDoneContacts Then
                    _BlDoneContacts = True
                    Dim oContacts = Await FEB2.Contacts.All(exs, GlobalVariables.Emp, _Location)
                    If exs.Count = 0 Then
                        Xl_AreaContacts1.Load(oContacts)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Albarans
                If Not _BlDoneAlbarans Then
                    _BlDoneAlbarans = True
                    Dim oDeliveries = Await FEB2.Deliveries.All(exs, GlobalVariables.Emp, _Location)
                    If exs.Count = 0 Then
                        Xl_AreaDeliveries1.Load(oDeliveries)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.BankBranches
                If Not _BlDoneBankBranches Then
                    _BlDoneBankBranches = True
                    Dim oBankBranches = Await FEB2.BankBranches.All(_Location, exs)
                    If exs.Count = 0 Then
                        Xl_LocationBankBranches1.Load(oBankBranches)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Async Sub Xl_LocationBankBranches1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_LocationBankBranches1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oBankBranches = Await FEB2.BankBranches.All(_Location, exs)
        If exs.Count = 0 Then
            Xl_LocationBankBranches1.Load(oBankBranches)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class