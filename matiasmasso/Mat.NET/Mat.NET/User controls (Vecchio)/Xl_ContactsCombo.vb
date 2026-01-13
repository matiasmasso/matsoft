Public Class Xl_ContactsCombo

    Private _values As List(Of Contact)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of Contact), Optional oSelectedContact As Contact = Nothing, Optional sNullText As String = "")
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllContacts(sNullText))
        If values IsNot Nothing Then
            For Each oContact As Contact In values
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

    Public ReadOnly Property Value As Contact
        Get
            Dim retval As Contact = Nothing
            Dim oItem As ControlItem = ComboBox1.SelectedItem
            If oItem IsNot Nothing Then
                retval = oItem.Source
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
        Public Property Source As Contact

        Public Property Nom As String

        Public Sub New(Optional oContact As Contact = Nothing)
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
                _Source = New Contact(oGuidNom.Guid)
                With _Source
                    _Nom = oGuidNom.Nom
                End With
            End If
        End Sub

        Shared Function AllContacts(Optional sNullText As String = "") As ControlItem
            Dim oGuidNom As New DTOGuidNom(Guid.NewGuid)
            If sNullText > "" Then
                oGuidNom.Nom = BLL.BLLSession.Current.Lang.Tradueix("(todos los clientes)", "(tots els clients)", "(any customer)")
            End If
            Dim retval As New ControlItem(oGuidNom)
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class
