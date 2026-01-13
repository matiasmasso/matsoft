Public Class Xl_RepsCombo
    Private _values As List(Of DTORep)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTORep), Optional oSelectedRep As DTORep = Nothing)
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllReps)
        For Each oRep As DTORep In values
            Dim oControlItem As New ControlItem(oRep)
            _ControlItems.Add(oControlItem)
        Next
        With ComboBox1
            .DataSource = _ControlItems
            .DisplayMember = "Nom"
        End With

        'set selected rep
        If oSelectedRep IsNot Nothing Then
            For i As Integer = 1 To _ControlItems.Count - 1
                If _ControlItems(i).Source.Guid.Equals(oSelectedRep.Guid) Then
                    ComboBox1.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTORep
        Get
            Dim retval As DTORep = Nothing
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
        Public Property Source As DTORep

        Public Property Nom As String

        Public Sub New(Optional oRep As DTORep = Nothing)
            MyBase.New()
            If oRep IsNot Nothing Then
                _Source = oRep
                With _Source
                    _Nom = oRep.NicknameOrNom()
                End With
            End If
        End Sub

        Shared Function AllReps() As ControlItem
            Dim retval As New ControlItem
            retval.Nom = Current.Session.Lang.Tradueix("(todos los representantes)", "(tots els representants)", "(all reps)")
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
