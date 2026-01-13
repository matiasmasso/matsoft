Public Class Xl_ContactsCombo

    Private _values As List(Of DTOContact)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOContact), Optional oSelectedContact As DTOContact = Nothing, Optional sNullText As String = "")
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllContacts(sNullText))
        If values IsNot Nothing Then
            For Each oContact As DTOContact In values
                Dim oControlItem As New ControlItem(oContact)
                _ControlItems.Add(oControlItem)
            Next
            With ComboBox1
                .DataSource = _ControlItems
                .DisplayMember = "Nom"
            End With

            'set selected Contact
            If oSelectedContact IsNot Nothing Then
                For i As Integer = 1 To _ControlItems.Count - 1
                    If _ControlItems(i).Source.Guid.Equals(oSelectedContact.Guid) Then
                        ComboBox1.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End If
        _AllowEvents = True
    End Sub

    Public Shadows Sub Load(values As List(Of DTOGuidNom), Optional oSelectedValue As Guid = Nothing, Optional sNullText As String = "")
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllContacts(sNullText))
        If values IsNot Nothing Then
            For Each oContact As DTOGuidNom In values
                Dim oControlItem As New ControlItem(oContact)
                _ControlItems.Add(oControlItem)
            Next
            With ComboBox1
                .DataSource = _ControlItems
                .DisplayMember = "Nom"
            End With

            'set selected Contact
            If oSelectedValue <> Nothing Then
                ComboBox1.SelectedValue = oSelectedValue
            End If
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOContact
        Get
            Dim retval As DTOContact = Nothing
            Dim oItem As ControlItem = ComboBox1.SelectedItem
            If oItem IsNot Nothing Then
                If oItem.Source IsNot Nothing Then
                    If Not oItem.Source.Guid.Equals(Guid.Empty) Then
                        retval = oItem.Source
                    End If
                End If
            End If
            Return retval
        End Get
    End Property

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOContact

        Public Property Nom As String

        Public Sub New(Optional oContact As DTOContact = Nothing)
            MyBase.New()
            If oContact IsNot Nothing Then
                _Source = oContact
                With _Source
                    _Nom = .Nom
                End With
            End If
        End Sub

        Public Sub New(Optional oGuidNom As DTOGuidNom = Nothing)
            MyBase.New()
            If oGuidNom IsNot Nothing Then
                _Source = New DTOContact(oGuidNom.Guid)
                With _Source
                    _Nom = oGuidNom.Nom
                End With
            End If
        End Sub

        Shared Function AllContacts(Optional sNullText As String = "") As ControlItem
            Dim oGuidNom As New DTOGuidNom(Guid.Empty)
            If String.IsNullOrEmpty(sNullText) Then
                oGuidNom.Nom = Current.Session.Lang.Tradueix("(todos los clientes)", "(tots els clients)", "(any customer)")
            Else
                oGuidNom.Nom = sNullText
            End If
            Dim retval As New ControlItem(oGuidNom)
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
