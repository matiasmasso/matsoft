Public Class Menu_RepCertRetencio
    Inherits Menu_Base

    Private _RepCertRetencions As List(Of DTORepCertRetencio)
    Private _RepCertRetencio As DTORepCertRetencio


    Public Sub New(ByVal oRepCertRetencions As List(Of DTORepCertRetencio))
        MyBase.New()
        _RepCertRetencions = oRepCertRetencions
        If _RepCertRetencions IsNot Nothing Then
            If _RepCertRetencions.Count > 0 Then
                _RepCertRetencio = _RepCertRetencions.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oRepCertRetencio As DTORepCertRetencio)
        MyBase.New()
        _RepCertRetencio = oRepCertRetencio
        _RepCertRetencions = New List(Of DTORepCertRetencio)
        If _RepCertRetencio IsNot Nothing Then
            _RepCertRetencions.Add(_RepCertRetencio)
        End If
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Pdf())
        MyBase.AddMenuItem(MenuItem_CopyLink())
    End Sub

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Enabled = _RepCertRetencions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _RepCertRetencions.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Contact.Load(_RepCertRetencio.Rep, exs) Then
            Dim oPdf As New LegacyHelper.PdfRepCertRetencio(_RepCertRetencio)
            If Not Await UIHelper.ShowStreamAsync(exs, oPdf.DocFile()) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyLink()
        Dim url = FEB.RepCertRetencio.CertFactoryUrl(_RepCertRetencio)
        UIHelper.CopyLink(url)
    End Sub


End Class


