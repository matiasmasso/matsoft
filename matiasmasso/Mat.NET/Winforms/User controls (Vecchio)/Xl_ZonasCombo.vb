Public Class Xl_ZonasCombo

    Private _values As List(Of Zona)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOZona), Optional oSelectedArea As DTOArea = Nothing)
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllZonas)
        For Each oZona As DTOZona In values
            Dim oControlItem As New ControlItem(oZona)
            _ControlItems.Add(oControlItem)
        Next
        With ComboBox1
            .DataSource = _ControlItems
            .DisplayMember = "Nom"
        End With

        'set selected Zona
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

    Public ReadOnly Property Value As DTOZona
        Get
            Dim retval As DTOZona = Nothing
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
        Public Property Source As DTOZona

        Public Property Nom As String

        Public Sub New(Optional oZona As DTOZona = Nothing)
            MyBase.New()
            If oZona IsNot Nothing Then
                _Source = oZona
                With _Source
                    _Nom = .Nom
                End With
            End If
        End Sub

        Shared Function AllZonas() As ControlItem
            Dim retval As New ControlItem
            retval.Nom = BLL.BLLSession.Current.Lang.Tradueix("(todas las zonas)", "(totes les zones)", "(any area)")
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class

