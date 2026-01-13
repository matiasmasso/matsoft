

Public Class Xl_Laboral_Categoria
    Private mLaboralCategoria As DTOStaffCategory = Nothing
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property LaboralCategoria() As DTOStaffCategory
        Get
            Return mLaboralCategoria
        End Get
        Set(ByVal value As DTOStaffCategory)
            mLaboralCategoria = value
            If mLaboralCategoria Is Nothing Then
                TextBox1.Clear()
            Else
                TextBox1.Text = mLaboralCategoria.Nom
            End If
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim oFrm As New Frm_LaboralCategorias(Frm_LaboralCategorias.Modes.LookUp, mLaboralCategoria)
        'AddHandler oFrm.AfterSelect, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.LaboralCategoria = sender
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
