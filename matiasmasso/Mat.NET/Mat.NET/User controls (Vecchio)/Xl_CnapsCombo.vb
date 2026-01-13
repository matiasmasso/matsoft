Public Class Xl_CnapsCombo

    Private _values As List(Of DTOCnap)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOCnap), Optional oSelectedCnap As DTOCnap = Nothing)
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllCnaps)
        For Each oCnap As DTOCnap In values
            Dim oControlItem As New ControlItem(oCnap)
            _ControlItems.Add(oControlItem)
        Next
        With ComboBox1
            .DataSource = _ControlItems
            .DisplayMember = "Nom"
        End With

        'set selected Cnap
        If oSelectedCnap IsNot Nothing Then
            For i As Integer = 1 To _ControlItems.Count - 1
                If _ControlItems(i).Source.Guid.Equals(oSelectedCnap.Guid) Then
                    ComboBox1.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOCnap
        Get
            Dim retval As DTOCnap = Nothing
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
        Public Property Source As DTOCnap

        Public Property Nom As String

        Public Sub New(Optional oCnap As DTOCnap = Nothing)
            MyBase.New()
            If oCnap IsNot Nothing Then
                _Source = oCnap
            End If
        End Sub

        Shared Function AllCnaps() As ControlItem
            Dim retval As New ControlItem
            retval.Nom = BLL.BLLSession.Current.Lang.Tradueix("(todos los Cnap)", "(tots els Cnap)", "(any Cnap)")
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class


