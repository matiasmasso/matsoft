

Public Class Xl_NIF
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Nif() As DTONifOld
        Get

            Dim retval = DTONifOld.Factory(TextBox1.Text)
            Return retval
        End Get
        Set(ByVal value As DTONifOld)
            TextBox1.Text = value.Value
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            If value <> Nothing Then
                TextBox1.Text = DTONifOld.CleanNif(value)
            End If
        End Set
    End Property

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Dim oNIF = DTONifOld.Factory(TextBox1.Text)
        MsgBox(DTONifOld.ValidationResultString(oNIF, DTOApp.current.lang))
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim oNIF = DTONifOld.Factory(TextBox1.Text)
        PictureBoxWarn.Visible = (DTONifOld.ValidationResult(oNIF) <> DTONifOld.Errors.Ok)
        RaiseEvent Changed(Me, New EventArgs)
    End Sub
End Class
