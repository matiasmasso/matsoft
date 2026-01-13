Public Class Xl_CountriesCombo
    Private _values As List(Of DTOCountry)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOCountry), Optional oSelectedArea As DTOArea = Nothing)
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllCountrys)
        For Each oCountry As DTOCountry In values
            Dim oControlItem As New ControlItem(oCountry)
            _ControlItems.Add(oControlItem)
        Next
        With ComboBox1
            .DataSource = _ControlItems
            .DisplayMember = "Nom"
        End With

        'set selected Country
        If oSelectedArea IsNot Nothing Then
            For i As Integer = 1 To _ControlItems.Count - 1
                If _ControlItems(i).Source.Guid.Equals(oSelectedArea.Guid) Then
                    ComboBox1.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOCountry
        Get
            Dim retval As DTOCountry = Nothing
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
        Public Property Source As DTOCountry

        Public Property Nom As String

        Public Sub New(Optional oCountry As DTOCountry = Nothing)
            MyBase.New()
            If oCountry IsNot Nothing Then
                _Source = oCountry
                With _Source
                    _Nom = .LangNom.Tradueix(Current.Session.Lang)
                End With
            End If
        End Sub

        Shared Function AllCountrys() As ControlItem
            Dim retval As New ControlItem
            retval.Nom = Current.Session.Lang.Tradueix("(todos los paises)", "(tots els paisos)", "(any country)")
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
