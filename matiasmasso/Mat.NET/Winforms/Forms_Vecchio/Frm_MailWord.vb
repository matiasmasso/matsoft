
Imports Microsoft.Office.Interop

Public Class Frm_MailWord
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents LabelNom As System.Windows.Forms.Label
    Friend WithEvents Xl_Langs1 As Xl_Langs_Old
    Friend WithEvents TextBoxPath As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonPath As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LabelNom = New System.Windows.Forms.Label
        Me.Xl_Langs1 = New Xl_Langs_Old
        Me.TextBoxPath = New System.Windows.Forms.TextBox
        Me.ButtonPath = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LabelNom
        '
        Me.LabelNom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelNom.Location = New System.Drawing.Point(8, 8)
        Me.LabelNom.Name = "LabelNom"
        Me.LabelNom.Size = New System.Drawing.Size(400, 20)
        Me.LabelNom.TabIndex = 0
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Location = New System.Drawing.Point(416, 8)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(48, 21)
        Me.Xl_Langs1.TabIndex = 1
        Me.Xl_Langs1.Tag = "Idioma"
        '
        'TextBoxPath
        '
        Me.TextBoxPath.Location = New System.Drawing.Point(8, 64)
        Me.TextBoxPath.Name = "TextBoxPath"
        Me.TextBoxPath.Size = New System.Drawing.Size(400, 20)
        Me.TextBoxPath.TabIndex = 2
        Me.TextBoxPath.Text = ""
        '
        'ButtonPath
        '
        Me.ButtonPath.Location = New System.Drawing.Point(416, 64)
        Me.ButtonPath.Name = "ButtonPath"
        Me.ButtonPath.Size = New System.Drawing.Size(48, 24)
        Me.ButtonPath.TabIndex = 3
        Me.ButtonPath.Text = "..."
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(368, 112)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(96, 24)
        Me.ButtonOk.TabIndex = 4
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'Frm_MailWord
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 150)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ButtonPath)
        Me.Controls.Add(Me.TextBoxPath)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.LabelNom)
        Me.Name = "Frm_MailWord"
        Me.Text = "CORRESPONDENCIA "
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mMail As Mail
    Private sTemplatesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.Templates)

    Public WriteOnly Property Mail() As Mail
        Set(ByVal Value As Mail)
            mMail = Value
            LabelNom.Text = mMail.Contacts(0).FullNom
            Xl_Langs1.Lang = mMail.Contacts(0).Lang
            Dim sPath As String = GetSetting("MAT.NET", "Mail", "LastTemplate")
            If sPath > "" Then
                TextBoxPath.Text = sPath
            End If
            ButtonOk.Enabled = sPath > ""
        End Set
    End Property


    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        WordFax()
        SaveSetting("MAT.NET", "Mail", "LastTemplate", TextBoxPath.Text)
        Me.Close()
    End Sub

    Private Sub WordFax()
        Dim oContact As DTOContact = mMail.Contacts(0)
        Dim oLang As DTOLang = Xl_Langs1.Lang

        Dim ObjWord As Word.Application
        Dim oDoc As Word.Document

        Dim StTemplate As String = TextBoxPath.Text

        ObjWord = New Word.Application
        oDoc = ObjWord.Documents.Add(StTemplate)
        oDoc.Activate()

        With oDoc
            Select Case oLang.Id
                Case DTOLang.Ids.CAT
                    .AttachedTemplate.LanguageID = Word.WdLanguageID.wdCatalan
                    .Range.LanguageID = Word.WdLanguageID.wdCatalan
                Case DTOLang.Ids.ENG
                    .AttachedTemplate.LanguageID = Word.WdLanguageID.wdEnglishUS
                    .Range.LanguageID = Word.WdLanguageID.wdEnglishUS
                Case Else
                    .AttachedTemplate.LanguageID = Word.WdLanguageID.wdSpanish
                    .Range.LanguageID = Word.WdLanguageID.wdSpanish
            End Select

            Dim sFullAdr As String = mMail.Atn
            If sFullAdr > "" Then sFullAdr += vbCrLf
            sFullAdr += BLLAddress.FullText(oContact.Address)
            '.Bookmarks("HEADER").Range.InsertFile MyGlobal.Funcs.Ap.SrvrPath & "Company\maxi\Emp" & MyGlobal.emp.ID & "/header.doc"
            SetBkm(oDoc, "NOM", BLLContact.NomComercialOrDefault(oContact))
            SetBkm(oDoc, "FULLADR", sFullAdr)
            SetBkm(oDoc, "FCH", mMail.Fch)
            SetBkm(oDoc, "ASN", mMail.Subject)
            SetBkm(oDoc, "REF", mMail.Id)
            SetBkm(oDoc, "FAX", "") 'oContact.DefaultFax)
            SetBkm(oDoc, "ATN", mMail.Atn)
            SetBkm(oDoc, "ORG", BLLApp.Emp.Org.Nom)
            SetBkm(oDoc, "FOOTER_REF", mMail.Id)

            SetBkm(oDoc, "labelFAX", oLang.Tradueix("fax destinatario", "fax destinatari", "fax number"))
            SetBkm(oDoc, "labelNOM", oLang.Tradueix("destinatario", "destinatari", "fax to"))
            SetBkm(oDoc, "labelFCH", oLang.Tradueix("fecha", "data", "date"))
            SetBkm(oDoc, "labelATN", oLang.Tradueix("a la atención de", "a la atenció de", "attn."))
            SetBkm(oDoc, "labelASN", oLang.Tradueix("asunto", "assumpte:", "subject"))
            SetBkm(oDoc, "labelREF", oLang.Tradueix("n/ref.", "n/ref.", "our ref."))
            SetBkm(oDoc, "labelBYE", oLang.Tradueix("Saludos", "Cordialment", "Best regards"))
        End With

        oDoc.Activate()
        ObjWord.Visible = True
        ObjWord = Nothing


        With ObjWord
        End With
    End Sub

    Private Sub SetBkm(ByVal oDoc As Word.Document, ByVal sKey As String, ByVal sValue As String)
        If oDoc.Bookmarks.Exists(sKey) Then
            Dim oBkm As Word.Bookmark = oDoc.Bookmarks(sKey)
            If Not oBkm Is Nothing Then
                oBkm.Range.Text = sValue
            End If
        End If
    End Sub

    Private Sub ButtonPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPath.Click
        Dim oDlg As New OpenFileDialog
        Dim oResult As DialogResult
        Dim sInitDir As String = ""
        If TextBoxPath.Text > "" Then
            Dim oLastPath As New System.IO.FileInfo(TextBoxPath.Text)
            If oLastPath.Exists Then
                sInitDir = oLastPath.DirectoryName
            Else
                sInitDir = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End If
        End If

        With oDlg
            .Title = "OBRIR PLANTILLA CORRESPONDÈNCIA"
            .Filter = "plantilles Word 2007 (*.dotx)|*.dotx|plantilles Word 2003 (*.dot)|*.dot|tots els arxius|*.*"
            .InitialDirectory = TextBoxPath.Text
            oResult = .ShowDialog
            Select Case oResult
                Case DialogResult.OK
                    TextBoxPath.Text = .FileName
            End Select
        End With

        ButtonOk.Enabled = TextBoxPath.Text > ""
    End Sub

    Private Sub TextBoxPath_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxPath.TextChanged
        ButtonOk.Enabled = TextBoxPath.Text > ""
    End Sub
End Class
