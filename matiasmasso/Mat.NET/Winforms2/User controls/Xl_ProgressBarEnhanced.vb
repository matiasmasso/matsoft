Public Class Xl_ProgressBarEnhanced
    Inherits PictureBox

    Public Sub New()
        Me.BorderStyle = BorderStyle.FixedSingle
        Me.Font = New Font("Segoe UI Semibold", 8)
    End Sub

    Public Sub ShowProgress(ByVal min As Long, ByVal max As Long, ByVal value As Long, ByVal msg As String, ByRef CancelRequest As Boolean)
        MyBase.Visible = True
        min = Math.Min(min, value)
        max = Math.Max(max, value)
        Dim percent As Integer = 100 * ((value - min)) / (max - min)
        Dim oGraphics = Graphics.FromHwnd(MyBase.Handle)
        DrawBar(oGraphics, percent)
        DrawMessage(oGraphics, msg, percent)
        Application.DoEvents()
    End Sub

    Private Sub DrawMessage(oGraphics As Graphics, msg As String, percent As Integer)
        'oGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
        Using sf As StringFormat = New StringFormat()
            sf.LineAlignment = StringAlignment.Center
            If String.IsNullOrEmpty(msg) Then
                sf.Alignment = StringAlignment.Center
                msg = String.Format("{0}%", percent)
            Else
                msg = String.Format("{0}  {1}%", msg, percent)
            End If
            oGraphics.DrawString(msg, Me.Font, Brushes.Black, MyBase.ClientRectangle, sf)
        End Using
    End Sub

    Private Sub DrawBar(oGraphics As Graphics, percent As Integer)
        oGraphics.Clear(SystemColors.ControlLight)
        Dim wid As Integer = CInt((percent * MyBase.ClientSize.Width / 100))
        oGraphics.FillRectangle(Brushes.LightGreen, 0, 0, wid, MyBase.ClientSize.Height)
    End Sub

    Private Sub Xl_ProgressBarEnhanced_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle,
               SystemColors.ControlLight, 0, ButtonBorderStyle.None, ' Left
               Color.DimGray, 0.5, ButtonBorderStyle.Solid, ' Top
               SystemColors.ControlLight, 0, ButtonBorderStyle.None, ' Right
               SystemColors.ControlLight, 0, ButtonBorderStyle.None) ' bottom
    End Sub
End Class
