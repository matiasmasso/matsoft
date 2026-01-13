Public Class PdfTemplateLlistat
    Inherits _BaseC1PdfDocument

    Property Title As String

    Public Sub New()
        MyBase.New("")
        YHeader = 0
        YTitle = 10
        MyBase._YColumnHeaders = 20
        _LeftMargin = 20
    End Sub

    Protected Shadows Sub NewPage()
        MyBase.NewPage()
        Y = YHeader
        DrawHeader()
    End Sub

    Public Sub DrawHeader()
        DrawTitle()
        DrawPageCount()
        Y += 20
        MyBase.DrawColumnHeaders()
    End Sub

    Private Shadows Sub DrawTitle()
        Y = YTitle
        X = _LeftMargin
        Dim PageUtilWidth As Integer = _RightMargin - _LeftMargin
        MyBase.DrawLine(New Pen(Brushes.Black), X, Y, _RightMargin, Y)
        MyBase.DrawString(_Title, PageUtilWidth, PdfColumn.HorizontalAlignments.Left)
        Y += MyBase.Font.Height
        MyBase.DrawLine(New Pen(Brushes.Black), X, Y, _RightMargin, Y)
    End Sub

    Private Sub DrawPageCount()
        Y = YTitle
        X = _LeftMargin
        Dim PageUtilWidth As Integer = _RightMargin - _LeftMargin
        MyBase.DrawString(MyBase.Pages.Count, PageUtilWidth, PdfColumn.HorizontalAlignments.Right)
    End Sub


End Class
