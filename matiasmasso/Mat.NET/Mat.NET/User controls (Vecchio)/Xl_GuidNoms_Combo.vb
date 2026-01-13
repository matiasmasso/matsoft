Public Class Xl_GuidNoms_Combo
    Inherits ComboBox

    Private _GuidNoms As List(Of DTOGuidNom)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Shadows Sub load(oValues As List(Of DTOGuidNom), Optional oSelectedValue As Guid = Nothing, Optional sNullText As String = "")
        _AllowEvents = False
        _GuidNoms = oValues
        If sNullText > "" Then
            Dim oNullValue As New DTOGuidNom(Guid.Empty, sNullText)
            _GuidNoms.Insert(0, oNullValue)
        End If

        MyBase.DataSource = _GuidNoms
        MyBase.DisplayMember = "Nom"
        MyBase.ValueMember = "Guid"

        If oSelectedValue <> Nothing Then
            MyBase.SelectedValue = oSelectedValue
        Else
            MyBase.SelectedIndex = 0
        End If

        _AllowEvents = True
    End Sub

    Public Function Value() As DTOGuidNom
        Dim retval As DTOGuidNom = Nothing
        If MyBase.SelectedValue <> Guid.Empty Then
            retval = MyBase.SelectedItem
        End If
        Return retval
    End Function

    Private Sub Xl_GuidNoms_Combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(MyBase.SelectedItem))
        End If
    End Sub

End Class
