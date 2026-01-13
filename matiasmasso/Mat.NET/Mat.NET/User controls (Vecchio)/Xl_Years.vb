Public Class Xl_Years
    Private _values As List(Of Integer)
    Private _AllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of Integer), Optional iCurrentValue As Integer = 0)
        _values = values
        If _values IsNot Nothing Then
            If _values.Count > 0 Then
                ComboBox1.DataSource = _values
                If iCurrentValue = 0 Then
                    ComboBox1.SelectedIndex = 0
                Else
                    ComboBox1.SelectedItem = Value
                End If
                SetButtons()
            End If
        End If
        _AllowEvents = True
    End Sub

    Public Function Exercici() As DTOExercici
        Dim retval As New DTOExercici
        retval.Emp = BLL.BLLApp.Emp
        retval.Year = Value
        Return retval
    End Function

    Public Property Value As Integer
        Get
            Return ComboBox1.SelectedItem
        End Get
        Set(value As Integer)
            ComboBox1.SelectedItem = value
        End Set
    End Property

    Private Sub ButtonFirst_Click(sender As Object, e As EventArgs) Handles ButtonFirst.Click
        ComboBox1.SelectedIndex = _values.Count - 1
        SetButtons()
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        ComboBox1.SelectedIndex -= 1
        SetButtons()
    End Sub

    Private Sub ButtonPrevious_Click(sender As Object, e As EventArgs) Handles ButtonPrevious.Click
        ComboBox1.SelectedIndex += 1
        SetButtons()
    End Sub

    Private Sub ButtonLast_Click(sender As Object, e As EventArgs) Handles ButtonLast.Click
        ComboBox1.SelectedIndex = 0
        SetButtons()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub SetButtons()
        If ComboBox1.SelectedIndex = 0 Then
            ButtonLast.Enabled = False
            ButtonNext.Enabled = False
        Else
            ButtonLast.Enabled = True
            ButtonNext.Enabled = True
        End If

        If ComboBox1.SelectedIndex = _values.Count - 1 Then
            ButtonFirst.Enabled = False
            ButtonPrevious.Enabled = False
        Else
            ButtonFirst.Enabled = True
            ButtonPrevious.Enabled = True
        End If

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub
End Class
