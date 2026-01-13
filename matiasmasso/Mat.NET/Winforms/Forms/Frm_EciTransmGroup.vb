Public Class Frm_EciTransmGroup
    Private _ECITransmGroup As DTOECITransmGroup
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOECITransmGroup)
        MyBase.New
        InitializeComponent()
        _ECITransmGroup = value
    End Sub

    Private Sub Frm_EciTransmGroup_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _ECITransmGroup Is Nothing Then
            TextBoxNom.Clear()
            Xl_Contact2Platform.Contact = Nothing
            CheckBoxCentres.Checked = False
            Xl_Contacts_Editable1.Load(New List(Of DTOContact))
        Else
            With _ECITransmGroup
                TextBoxNom.Text = .Nom
                Xl_Contact2Platform.Contact = .Platform
                CheckBoxCentres.Checked = .Items.Count > 0
                Xl_Contacts_Editable1.Load(DTOECITransmGroup.Contacts(_ECITransmGroup))
            End With
        End If
        Xl_Contact2Platform.Enabled = True
        Xl_Contacts_Editable1.Enabled = CheckBoxCentres.Checked
        _AllowEvents = True
    End Sub

    Private Async Sub Control_Changed(sender As Object, e As EventArgs) Handles _
    TextBoxNom.TextChanged
        If _AllowEvents Then
            Await SaveGroup()
        End If
    End Sub

    Private Async Sub CheckBoxCentres_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCentres.CheckedChanged
        If _AllowEvents Then
            Xl_Contacts_Editable1.Enabled = CheckBoxCentres.Checked
            If Not CheckBoxCentres.Checked Then
                Await SaveGroup()
            End If
        End If
    End Sub

    Private Async Sub Xl_Contacts_Editable1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Editable1.AfterUpdate
        Await SaveGroup()
    End Sub

    Private Async Function SaveGroup() As Task
        Dim oGroup As DTOECITransmGroup = _ECITransmGroup
        Dim oContacts As List(Of DTOContact) = Xl_Contacts_Editable1.Values

        With oGroup
            .Nom = TextBoxNom.Text

            .Platform = Xl_Contact2Platform.Contact

            .Items = New List(Of DTOECITransmCentre)
            If CheckBoxCentres.Checked Then
                For Each oContact As DTOContact In oContacts
                    Dim item As New DTOECITransmCentre()
                    item.Parent = oGroup
                    item.Centre = oContact
                    .Items.Add(item)
                Next
            End If

            Dim exs As New List(Of Exception)
            If Await FEB2.ECITransmGroup.Update(oGroup, exs) Then
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Function
End Class