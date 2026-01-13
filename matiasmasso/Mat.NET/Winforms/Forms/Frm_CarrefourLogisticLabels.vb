Public Class Frm_CarrefourLogisticLabels

    Private _items As List(Of DTOCarrefourItem)

    Public Sub New(items As List(Of DTOCarrefourItem))
        MyBase.New
        InitializeComponent()
        _items = items
    End Sub

    Private Sub Frm_CarrefourLogisticLabels_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_CarrefourLogisticLabels1.Load(_items)
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim values As List(Of DTOCarrefourItem) = Xl_CarrefourLogisticLabels1.Values
        Dim oPdf As New LegacyHelper.PdfLogisticLabelsCarrefour(values)
        Dim oByteArray() As Byte = oPdf.Stream

        Dim exs As New List(Of Exception)
        Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray)
        If oPdf.exs.Count = 0 Then
            If Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(oPdf.exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class