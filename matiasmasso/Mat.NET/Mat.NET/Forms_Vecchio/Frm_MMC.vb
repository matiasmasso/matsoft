

Public Class Frm_MMC
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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelParent As System.Windows.Forms.Label
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAction As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_ImageBig As Xl_Image
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Rols_Allowed1 As Xl_Rols_Allowed
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_MMC))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_ImageBig = New Mat.Net.Xl_Image()
        Me.TextBoxAction = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelParent = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Rols_Allowed1 = New Mat.Net.Xl_Rols_Allowed()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(280, 216)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.ComboBoxCod)
        Me.TabPage1.Controls.Add(Me.Xl_ImageBig)
        Me.TabPage1.Controls.Add(Me.TextBoxAction)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.LabelParent)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(272, 190)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "GENERAL"
        '
        'Xl_ImageBig
        '
        Me.Xl_ImageBig.Bitmap = CType(resources.GetObject("Xl_ImageBig.Bitmap"), System.Drawing.Bitmap)
        Me.Xl_ImageBig.EmptyImageLabelText = ""
        Me.Xl_ImageBig.Location = New System.Drawing.Point(64, 120)
        Me.Xl_ImageBig.MaxHeight = 0
        Me.Xl_ImageBig.MaxWidth = 0
        Me.Xl_ImageBig.Name = "Xl_ImageBig"
        Me.Xl_ImageBig.Size = New System.Drawing.Size(48, 48)
        Me.Xl_ImageBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_ImageBig.TabIndex = 10
        Me.Xl_ImageBig.ZipStream = Nothing
        '
        'TextBoxAction
        '
        Me.TextBoxAction.Location = New System.Drawing.Point(64, 87)
        Me.TextBoxAction.Name = "TextBoxAction"
        Me.TextBoxAction.Size = New System.Drawing.Size(184, 20)
        Me.TextBoxAction.TabIndex = 7
        Me.TextBoxAction.Visible = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "acció:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(64, 32)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(184, 20)
        Me.TextBoxNom.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "nom:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "parent:"
        '
        'LabelParent
        '
        Me.LabelParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelParent.Location = New System.Drawing.Point(64, 8)
        Me.LabelParent.Name = "LabelParent"
        Me.LabelParent.Size = New System.Drawing.Size(176, 18)
        Me.LabelParent.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Rols_Allowed1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(272, 190)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PERMISOS"
        '
        'Xl_Rols_Allowed1
        '
        Me.Xl_Rols_Allowed1.Location = New System.Drawing.Point(8, 8)
        Me.Xl_Rols_Allowed1.Name = "Xl_Rols_Allowed1"
        Me.Xl_Rols_Allowed1.Size = New System.Drawing.Size(256, 168)
        Me.Xl_Rols_Allowed1.TabIndex = 0
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(208, 248)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(80, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(120, 248)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(8, 248)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonDel.TabIndex = 5
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"(seleccionar codi)", "carpeta", "item"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(64, 60)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxCod.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "codi:"
        '
        'Frm_MMC
        '
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_MMC"
        Me.Text = "MMC"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mMMC As MMC
    Private mDirtyRols As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)


    Public Sub New(oMMC As MMC)
        MyBase.New()
        Me.InitializeComponent()
        mMMC = oMMC
        SetControls()
    End Sub

    Public ReadOnly Property MMC() As MMC
        Get
            Return mMMC
        End Get
    End Property

    Private Sub SetControls()
        With mMMC
            Me.Text = "MMC " & .Id
            If .Parent.Id = 0 Then
                LabelParent.Text = "(arrel)"
            Else
                LabelParent.Text = .Parent.Id & " - " & MMC.Parent.Nom
            End If
            TextBoxNom.Text = .Nom
            ComboBoxCod.SelectedIndex = .Cod
            TextBoxAction.Text = .Action
            TextBoxAction.Enabled = (.Cod = MaxiSrvr.MMC.Cods.Item)
            If Not .ImgBig Is Nothing Then Xl_ImageBig.Bitmap = .ImgBig
            'If Not .ImgBigSelected Is Nothing Then Xl_ImageBigSelected.Bitmap = .ImgBigSelected
            Xl_Rols_Allowed1.Load(.Rols)
            ButtonDel.Enabled = (.Id > 0)
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub EnableButtons(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles TextBoxNom.TextChanged, _
     TextBoxAction.TextChanged
        Dim BlEnabled As Boolean = True
        If TextBoxNom.Text = "" Then BlEnabled = False
        ButtonOk.Enabled = BlEnabled
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMMC
            .Nom = TextBoxNom.Text
            .Cod = ComboBoxCod.SelectedIndex
            .Action = TextBoxAction.Text
            .ImgBig = Xl_ImageBig.Bitmap
            '.ImgBigSelected = Xl_ImageBigSelected.Bitmap
            If mDirtyRols Then .Rols = Xl_Rols_Allowed1.Rols
            .Update()
            RaiseEvent AfterUpdate(mMMC, EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub Xl_Rols_Allowed1_AfterUpdate() Handles Xl_Rols_Allowed1.AfterUpdate
        EnableButtons(Me, New System.EventArgs)
        mDirtyRols = True
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem l'accés directe " & mMMC.Nom & "?", MsgBoxStyle.OKCancel, "MAT.NET")
        If rc = MsgBoxResult.OK Then
            mMMC.delete()
            MsgBox("Accés eliminat", MsgBoxStyle.Exclamation, "MAT.NET")
            Me.Close()
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Private Sub ComboBoxCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        Dim oCod As MMC.Cods = ComboBoxCod.SelectedIndex
        TextBoxAction.Visible = (oCod = MaxiSrvr.MMC.Cods.Item)
        TextBoxAction.Enabled = (oCod = MaxiSrvr.MMC.Cods.Item)
    End Sub
End Class
