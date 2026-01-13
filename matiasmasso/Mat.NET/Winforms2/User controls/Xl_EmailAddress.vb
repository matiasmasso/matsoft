Public Class Xl_EmailAddress
    Public Event onValidation(sender As Object, e As MatEventArgs)
    Public Shadows Event Validated(sender As Object, e As EventArgs)

    Public Shadows Sub Load(sValue As String)
        TextBox1.Text = sValue
        Validate()
    End Sub

    Public ReadOnly Property Value As String
        Get
            Return TextBox1.Text
        End Get
    End Property

    Public Function Nickname() As String
        Dim retval As String = ""
        Dim chevronIdx = TextBox1.Text.IndexOf("<")
        If chevronIdx > 0 Then
            retval = TextBox1.Text.Substring(0, chevronIdx)
        End If
        Return retval
    End Function

    Public Function EmailAddress() As String
        Dim retval As String = ""
        Dim openChevronIdx = TextBox1.Text.IndexOf("<")
        If openChevronIdx > 0 Then
            retval = TextBox1.Text.Substring(openChevronIdx + 1)
            Dim closeChevronIdx = retval.IndexOf(">")
            If closeChevronIdx > 0 Then
                retval = retval.Substring(0, closeChevronIdx)
            End If
        End If
        Return retval
    End Function


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CheckSintaxis()
    End Sub

    Private Sub CheckSintaxis()
        Dim src As String = TextBox1.Text
        If src = "" Then
            PictureBox1.Visible = False
            TextBox1.BackColor = Color.White
            'RaiseEvent onValidation(Me, MatEventArgs.Empty)
        Else
            If DTOUser.CheckEmailSintaxis(src) Then
                PictureBox1.Visible = True
                PictureBox1.Image = My.Resources.vb
                TextBox1.BackColor = Color.White
                RaiseEvent onValidation(Me, New MatEventArgs(src))
            Else
                PictureBox1.Visible = False
                TextBox1.BackColor = Color.LightYellow
                'RaiseEvent onValidation(Me, MatEventArgs.Empty)
            End If
        End If

    End Sub



    Private Sub TextBox1_Validated(sender As Object, e As EventArgs) Handles TextBox1.Validated
        RaiseEvent Validated(sender, e)
    End Sub
End Class
