

Public Class Menu_Csa
    Private mCsas As Csas
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCsa As Csa)
        MyBase.New()
        mCsas = New Csas
        mCsas.Add(oCsa)
    End Sub


    Public Sub New(ByVal oCsas As Csas)
        MyBase.New()
        mCsas = oCsas
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), MenuItem_Print(), MenuItem_Preview(), MenuItem_File(), menuitem_FileSEPAB2B, MenuItem_Undo(), MenuItem_Despeses()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Print() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Print"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_Print
        Return oMenuItem
    End Function

    Private Function MenuItem_Preview() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Vista previa"
        'oMenuItem.Image = My.Resources.preview_16
        AddHandler oMenuItem.Click, AddressOf Do_Preview
        Return oMenuItem
    End Function

    Private Function MenuItem_File() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_File
        Return oMenuItem
    End Function

    Private Function menuitem_FileSEPAB2B() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer SEPA B2B"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_FileSEPAB2B
        Return oMenuItem
    End Function

    Private Function MenuItem_Undo() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir"
        oMenuItem.Image = My.Resources.UNDO
        AddHandler oMenuItem.Click, AddressOf Do_Undo
        Return oMenuItem
    End Function

    Private Function MenuItem_Despeses() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Despeses"
        If mCsas(0).Despeses IsNot Nothing Then
            If mCsas(0).Despeses.Val <> 0 Then oMenuItem.Enabled = False
        End If
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Despeses
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCsa(mCsas(0))
    End Sub

    Private Sub Do_Undo(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & CsasNums() & "?", MsgBoxStyle.OKCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mCsas.BackUp( exs) Then
                MsgBox("Remeses eliminades", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, e)
            Else
                MsgBox("No s'ha pogut retrocedir" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Preview(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintCsas(mCsas, maxisrvr.ReportDocument.PrintModes.Preview)
    End Sub

    Private Sub Do_Print(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintCsas(mCsas, maxisrvr.ReportDocument.PrintModes.Copia)
    End Sub

    Private Sub Do_File(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As Csa
        For Each oCsa In mCsas
            root.SaveCsaFile(oCsa)
        Next
    End Sub

    Private Sub Do_FileSEPAB2B(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as new list(Of Exception)
        If mCsas(0).validateSepaB2B( exs) Then
            Dim oCsa As Csa
            For Each oCsa In mCsas
                root.SaveCsaFile(oCsa, DTOCsa.FileFormats.SepaB2b)
            Next
        Else
            MsgBox( BLL.Defaults.ExsToMultiline(exs), vbExclamation, "MAT.NET")
        End If
    End Sub

       Private Sub Do_Despeses(ByVal sender As Object, ByVal e As System.EventArgs)
        If mCsas(0).descomptat Then
            root.ExeCsaDespeses(mCsas(0))
        Else


            Dim exs as New List(Of exception)
            mCsas(0).SetDespeses_AEB19(root.Usuari, exs)

        End If
    End Sub

    Private Function CsasNums() As String
        Select Case mCsas.Count
            Case 0
                Return "(cap remesa)"
            Case 1
                Return "Remesa " & mCsas(0).Id
            Case 2, 3, 4, 5
                Dim s As String = "Remeses "
                Dim i As Integer
                For i = 0 To mCsas.Count - 1
                    If i > 0 Then s = s & ","
                    s = s & mCsas(i).Id
                Next
                Return s
            Case Else
                Return "Remeses " & mCsas(0).Id & ",..."
        End Select
    End Function

    Private Sub ShowPdf()

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
