Imports System.ComponentModel

Public Class Frm_LongTask
    Private _Cancel As Boolean
    Private _BackgroundWorker As BackgroundWorker
    Private _DataSource As Dictionary(Of Guid, String)

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        ButtonOk.Enabled = False

        With ProgressBar1
            .Maximum = GetDataSourceCount()
            .Value = 0
        End With

        _BackgroundWorker = New BackgroundWorker
        _BackgroundWorker.WorkerReportsProgress = True
        _BackgroundWorker.WorkerSupportsCancellation = True
        AddHandler _BackgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _BackgroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _BackgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted

        'Dim oDoWorkEventArgs As New DoWorkEventArgs(_DataSource)
        _BackgroundWorker.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim idx As Integer
        Dim timestart As DateTime = Now
        'Dim SQL As String = "SELECT * FROM Bigfile ORDER BY FCH"
        Dim SQL As String = "SELECT * FROM PrNumeros WHERE PDF IS NOT NULL"
        _DataSource = New Dictionary(Of Guid, String)
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If _BackgroundWorker.CancellationPending Then Exit Do
            Dim oGuid As Guid = oDrd("Guid")

            Dim oByteArray As Byte()
            If Not IsDBNull(oDrd("Pdf")) Then
                oByteArray = oDrd("Pdf")
                Dim sHash As String = BLL.CryptoHelper.HashMD5(oByteArray)
                Dim exs As New List(Of Exception)

                Dim oDocFile As DTODocFile = Nothing
                If BLLDocFile.LoadFromStream(oByteArray, oDocFile, exs, "w.pdf") Then
                End If

                If Not BLL.BLLDocFile.Update(oDocFile, DTODocFile.Cods.RevistaPortada, oGuid, exs) Then
                    Stop
                End If
                ' _DataSource.Add(oGuid, sHash)
            End If
            idx += 1
            _BackgroundWorker.ReportProgress(idx)
        Loop
        oDrd.Close()
        Dim timeend As DateTime = Now
        Stop

        idx = 0
        For Each oPair As KeyValuePair(Of Guid, String) In _DataSource
            idx += 1
            SQL = "UPDATE BIGFILE SET HASH=@Hash WHERE GUID=@Guid"
            Dim exs As New List(Of Exception)
            Dim i As Integer = DAL.SQLHelper.ExecuteNonQuery(SQL, exs, "@Guid", oPair.Key.ToString, "@Hash", oPair.Value)
            _BackgroundWorker.ReportProgress(idx)
        Next
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        Dim iTot As Integer = ProgressBar1.Maximum
        Dim iPercent As Integer = 100 * e.ProgressPercentage / iTot
        TextBox1.Text = String.Format("{0}% {1} de {2}", iPercent, e.ProgressPercentage, iTot)
        TextBox2.Text = e.UserState
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        TextBox2.Text = "tasca completada"
        ButtonOk.Enabled = True
    End Sub

    Private Function GetDataSourceCount() As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT count(Guid) as Files FROM Bigfile"
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval = oDrd("Files")
        Loop
        oDrd.Close()
        Return retval
    End Function

    Private Function GetDataSource() As Dictionary(Of Guid, String)
        Dim retval As New Dictionary(Of Guid, String)
        Dim SQL As String = "SELECT * FROM Bigfile"
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oByteArray As Byte() = oDrd("Doc")
            Dim sHash As String = BLL.CryptoHelper.HashMD5(oByteArray)
            retval.Add(oGuid, sHash)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _BackgroundWorker.CancelAsync()
    End Sub
End Class