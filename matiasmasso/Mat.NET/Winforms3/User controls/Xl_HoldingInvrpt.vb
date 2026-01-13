Public Class Xl_HoldingInvrpt
    Private _value As DTO.Models.InvrptModel
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public ReadOnly Property Fch As Date
        Get
            Return Xl_LookupFch1.Fch
        End Get
    End Property

    Public Shadows Sub Load(value As DTO.Models.InvrptModel, Optional fch As Nullable(Of Date) = Nothing)
        _value = value
        ComboBoxMode.SelectedIndex = DTO.Models.InvrptModel.Modes.Catalog
        Xl_LookupFch1.AvailableFchs = value.Fchs.ToList()
        If (fch Is Nothing OrElse CDate(fch) = Nothing) AndAlso value.Fchs.Count > 0 Then fch = value.Fchs.First()
        Xl_LookupFch1.Fch = fch
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim mode As DTO.Models.InvrptModel.Modes = ComboBoxMode.SelectedIndex
        Xl_InvRpts1.Load(_value, mode)
    End Sub

    Private Sub ComboBoxMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMode.SelectedIndexChanged
        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub Xl_LookupFch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupFch1.AfterUpdate
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub
End Class
