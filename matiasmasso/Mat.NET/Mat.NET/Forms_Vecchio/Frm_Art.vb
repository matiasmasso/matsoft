

Public Class Frm_Art
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ImageListTabs As System.Windows.Forms.ImageList
    Friend WithEvents TabPageDownloads As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ProductDownloads1 As Xl_ProductDownloads
    Friend WithEvents TabPageOutlet As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxOutletQty As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxOutletDto As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxOutlet As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageSpares As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtsSpares As Xl_Arts
    Friend WithEvents TabPageAccessoris As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtsAccessories As Xl_Arts
    Friend WithEvents TabPageGrup As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Art_Group1 As Xl_Art_Group
    Friend WithEvents TabPageTendencia As System.Windows.Forms.TabPage
    Friend WithEvents PictureBoxTendencies As System.Windows.Forms.PictureBox
    Friend WithEvents TabPageLogistica As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtLogistics1 As Xl_Art_Logistics
    Friend WithEvents TabPageEdit As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Art_Edit1 As Xl_Art_Edit
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Art_Gral1 As Xl_Art_Gral
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Xl_Banc_Menu1 As Xl_Banc_Menu
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Art))
        Me.ImageListTabs = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Banc_Menu1 = New Xl_Banc_Menu()
        Me.TabPageDownloads = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Xl_ProductDownloads()
        Me.TabPageOutlet = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxOutlet = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxOutletDto = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxOutletQty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPageSpares = New System.Windows.Forms.TabPage()
        Me.Xl_ArtsSpares = New Xl_Arts()
        Me.TabPageAccessoris = New System.Windows.Forms.TabPage()
        Me.Xl_ArtsAccessories = New Xl_Arts()
        Me.TabPageGrup = New System.Windows.Forms.TabPage()
        Me.Xl_Art_Group1 = New Xl_Art_Group()
        Me.TabPageTendencia = New System.Windows.Forms.TabPage()
        Me.PictureBoxTendencies = New System.Windows.Forms.PictureBox()
        Me.TabPageLogistica = New System.Windows.Forms.TabPage()
        Me.Xl_ArtLogistics1 = New Xl_Art_Logistics()
        Me.TabPageEdit = New System.Windows.Forms.TabPage()
        Me.Xl_Art_Edit1 = New Xl_Art_Edit()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Xl_Art_Gral1 = New Xl_Art_Gral()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Panel1.SuspendLayout()
        Me.TabPageDownloads.SuspendLayout()
        Me.TabPageOutlet.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageSpares.SuspendLayout()
        Me.TabPageAccessoris.SuspendLayout()
        Me.TabPageGrup.SuspendLayout()
        Me.TabPageTendencia.SuspendLayout()
        CType(Me.PictureBoxTendencies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageLogistica.SuspendLayout()
        Me.TabPageEdit.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageListTabs
        '
        Me.ImageListTabs.ImageStream = CType(resources.GetObject("ImageListTabs.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListTabs.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListTabs.Images.SetKeyName(0, "STAR")
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 492)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(754, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(535, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(646, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TabPageDownloads
        '
        Me.TabPageDownloads.Controls.Add(Me.Xl_ProductDownloads1)
        Me.TabPageDownloads.Location = New System.Drawing.Point(4, 23)
        Me.TabPageDownloads.Name = "TabPageDownloads"
        Me.TabPageDownloads.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDownloads.Size = New System.Drawing.Size(734, 455)
        Me.TabPageDownloads.TabIndex = 9
        Me.TabPageDownloads.Text = "DESCARREGUES"
        Me.TabPageDownloads.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductDownloads1.Name = "Xl_ProductDownloads1"
        Me.Xl_ProductDownloads1.Size = New System.Drawing.Size(728, 449)
        Me.Xl_ProductDownloads1.TabIndex = 2
        '
        'TabPageOutlet
        '
        Me.TabPageOutlet.Controls.Add(Me.GroupBox1)
        Me.TabPageOutlet.Location = New System.Drawing.Point(4, 23)
        Me.TabPageOutlet.Name = "TabPageOutlet"
        Me.TabPageOutlet.Size = New System.Drawing.Size(734, 455)
        Me.TabPageOutlet.TabIndex = 8
        Me.TabPageOutlet.Text = "OUTLET"
        Me.TabPageOutlet.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBoxOutletQty)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxOutletDto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CheckBoxOutlet)
        Me.GroupBox1.Location = New System.Drawing.Point(154, 103)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(326, 138)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'CheckBoxOutlet
        '
        Me.CheckBoxOutlet.AutoSize = True
        Me.CheckBoxOutlet.Location = New System.Drawing.Point(6, 0)
        Me.CheckBoxOutlet.Name = "CheckBoxOutlet"
        Me.CheckBoxOutlet.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxOutlet.TabIndex = 0
        Me.CheckBoxOutlet.Text = "en liquidació a l'Outlet"
        Me.CheckBoxOutlet.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "descompte:"
        '
        'TextBoxOutletDto
        '
        Me.TextBoxOutletDto.Location = New System.Drawing.Point(119, 46)
        Me.TextBoxOutletDto.Name = "TextBoxOutletDto"
        Me.TextBoxOutletDto.Size = New System.Drawing.Size(33, 20)
        Me.TextBoxOutletDto.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(155, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "%"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(52, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "a partir de "
        '
        'TextBoxOutletQty
        '
        Me.TextBoxOutletQty.Location = New System.Drawing.Point(119, 72)
        Me.TextBoxOutletQty.Name = "TextBoxOutletQty"
        Me.TextBoxOutletQty.Size = New System.Drawing.Size(33, 20)
        Me.TextBoxOutletQty.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(158, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "unitats"
        '
        'TabPageSpares
        '
        Me.TabPageSpares.Controls.Add(Me.Xl_ArtsSpares)
        Me.TabPageSpares.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSpares.Name = "TabPageSpares"
        Me.TabPageSpares.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSpares.Size = New System.Drawing.Size(734, 455)
        Me.TabPageSpares.TabIndex = 7
        Me.TabPageSpares.Text = "RECANVIS"
        Me.TabPageSpares.UseVisualStyleBackColor = True
        '
        'Xl_ArtsSpares
        '
        Me.Xl_ArtsSpares.Location = New System.Drawing.Point(4, 6)
        Me.Xl_ArtsSpares.Name = "Xl_ArtsSpares"
        Me.Xl_ArtsSpares.Size = New System.Drawing.Size(727, 440)
        Me.Xl_ArtsSpares.TabIndex = 2
        '
        'TabPageAccessoris
        '
        Me.TabPageAccessoris.Controls.Add(Me.Xl_ArtsAccessories)
        Me.TabPageAccessoris.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAccessoris.Name = "TabPageAccessoris"
        Me.TabPageAccessoris.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAccessoris.Size = New System.Drawing.Size(734, 455)
        Me.TabPageAccessoris.TabIndex = 6
        Me.TabPageAccessoris.Text = "ACCESSORIS"
        Me.TabPageAccessoris.UseVisualStyleBackColor = True
        '
        'Xl_ArtsAccessories
        '
        Me.Xl_ArtsAccessories.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ArtsAccessories.Name = "Xl_ArtsAccessories"
        Me.Xl_ArtsAccessories.Size = New System.Drawing.Size(727, 449)
        Me.Xl_ArtsAccessories.TabIndex = 0
        '
        'TabPageGrup
        '
        Me.TabPageGrup.Controls.Add(Me.Xl_Art_Group1)
        Me.TabPageGrup.Location = New System.Drawing.Point(4, 23)
        Me.TabPageGrup.Name = "TabPageGrup"
        Me.TabPageGrup.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGrup.Size = New System.Drawing.Size(734, 455)
        Me.TabPageGrup.TabIndex = 5
        Me.TabPageGrup.Text = "GRUP"
        Me.TabPageGrup.UseVisualStyleBackColor = True
        '
        'Xl_Art_Group1
        '
        Me.Xl_Art_Group1.Location = New System.Drawing.Point(6, 19)
        Me.Xl_Art_Group1.Name = "Xl_Art_Group1"
        Me.Xl_Art_Group1.Size = New System.Drawing.Size(573, 431)
        Me.Xl_Art_Group1.TabIndex = 0
        '
        'TabPageTendencia
        '
        Me.TabPageTendencia.Controls.Add(Me.PictureBoxTendencies)
        Me.TabPageTendencia.Location = New System.Drawing.Point(4, 23)
        Me.TabPageTendencia.Name = "TabPageTendencia"
        Me.TabPageTendencia.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTendencia.Size = New System.Drawing.Size(734, 455)
        Me.TabPageTendencia.TabIndex = 3
        Me.TabPageTendencia.Text = "TENDENCIA"
        Me.TabPageTendencia.UseVisualStyleBackColor = True
        '
        'PictureBoxTendencies
        '
        Me.PictureBoxTendencies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxTendencies.Location = New System.Drawing.Point(3, 3)
        Me.PictureBoxTendencies.Name = "PictureBoxTendencies"
        Me.PictureBoxTendencies.Size = New System.Drawing.Size(728, 449)
        Me.PictureBoxTendencies.TabIndex = 0
        Me.PictureBoxTendencies.TabStop = False
        '
        'TabPageLogistica
        '
        Me.TabPageLogistica.Controls.Add(Me.Xl_ArtLogistics1)
        Me.TabPageLogistica.Location = New System.Drawing.Point(4, 23)
        Me.TabPageLogistica.Name = "TabPageLogistica"
        Me.TabPageLogistica.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLogistica.Size = New System.Drawing.Size(734, 455)
        Me.TabPageLogistica.TabIndex = 10
        Me.TabPageLogistica.Text = "LOGISTICA"
        Me.TabPageLogistica.UseVisualStyleBackColor = True
        '
        'Xl_ArtLogistics1
        '
        Me.Xl_ArtLogistics1.Location = New System.Drawing.Point(6, 24)
        Me.Xl_ArtLogistics1.Name = "Xl_ArtLogistics1"
        Me.Xl_ArtLogistics1.Size = New System.Drawing.Size(674, 408)
        Me.Xl_ArtLogistics1.TabIndex = 0
        '
        'TabPageEdit
        '
        Me.TabPageEdit.Controls.Add(Me.Xl_Art_Edit1)
        Me.TabPageEdit.Location = New System.Drawing.Point(4, 23)
        Me.TabPageEdit.Name = "TabPageEdit"
        Me.TabPageEdit.Size = New System.Drawing.Size(734, 455)
        Me.TabPageEdit.TabIndex = 1
        Me.TabPageEdit.Text = "EDITAR"
        Me.TabPageEdit.UseVisualStyleBackColor = True
        '
        'Xl_Art_Edit1
        '
        Me.Xl_Art_Edit1.Art = Nothing
        Me.Xl_Art_Edit1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Art_Edit1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Art_Edit1.Name = "Xl_Art_Edit1"
        Me.Xl_Art_Edit1.Size = New System.Drawing.Size(734, 455)
        Me.Xl_Art_Edit1.TabIndex = 0
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Xl_Art_Gral1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 23)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Size = New System.Drawing.Size(734, 455)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        Me.TabPageGral.UseVisualStyleBackColor = True
        '
        'Xl_Art_Gral1
        '
        Me.Xl_Art_Gral1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Art_Gral1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Art_Gral1.Name = "Xl_Art_Gral1"
        Me.Xl_Art_Gral1.Size = New System.Drawing.Size(734, 455)
        Me.Xl_Art_Gral1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPageEdit)
        Me.TabControl1.Controls.Add(Me.TabPageLogistica)
        Me.TabControl1.Controls.Add(Me.TabPageTendencia)
        Me.TabControl1.Controls.Add(Me.TabPageGrup)
        Me.TabControl1.Controls.Add(Me.TabPageAccessoris)
        Me.TabControl1.Controls.Add(Me.TabPageSpares)
        Me.TabControl1.Controls.Add(Me.TabPageOutlet)
        Me.TabControl1.Controls.Add(Me.TabPageDownloads)
        Me.TabControl1.ImageList = Me.ImageListTabs
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(742, 482)
        Me.TabControl1.TabIndex = 18
        '
        'Frm_Art
        '
        Me.ClientSize = New System.Drawing.Size(754, 523)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Art"
        Me.Text = "ARTICLE"
        Me.Panel1.ResumeLayout(False)
        Me.TabPageDownloads.ResumeLayout(False)
        Me.TabPageOutlet.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPageSpares.ResumeLayout(False)
        Me.TabPageAccessoris.ResumeLayout(False)
        Me.TabPageGrup.ResumeLayout(False)
        Me.TabPageTendencia.ResumeLayout(False)
        CType(Me.PictureBoxTendencies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageLogistica.ResumeLayout(False)
        Me.TabPageEdit.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mArt As Art
    Private mCancel As Boolean
    Private mChanged As Boolean
    Private mAllowEvents As Boolean
    Private mAllowEventsDownload As Boolean
    Private mDirty As Boolean
    Private mDirtyGroup As Boolean
    Private mDirtyStream As Boolean
    Private mMode As Modes
    Private CleanTab(20) As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Modes
        Browse
        Edit
    End Enum


    Private Enum Tabs
        General
        Edit
        Logistics
        Tendencia
        Grup
        Accessories
        Spares
        Outlet
        Downloads
        Galeria
    End Enum

    Private Enum ColsDownloads
        Guid
        Src
        Img
        Text
        Status
    End Enum


    Public Sub New(ByVal oArt As Art, Optional oMode As Modes = Modes.Browse)
        MyBase.New()
        InitializeComponent()
        mArt = oArt
        mMode = oMode
        refresca()
    End Sub

    Private Sub refresca()
        If mArt.Id = 0 Then
            Me.Text = "alta de nou article"
        Else
            Me.Text = "article " & mArt.Id & " " & mArt.Nom_ESP
        End If
        Xl_Art_Gral1.Art = mArt
        AddHandler Xl_Art_Edit1.AfterUpdate, AddressOf EnableButtons
        AddHandler Xl_Art_Edit1.AfterImageUpdate, AddressOf Xl_Art_Edit1_AfterImageUpdate

        SetGrupTabBackColor(mArt.ArtWiths.Count > 0)

        CheckBoxOutlet.Checked = mArt.Outlet
        If mArt.Outlet Then
            TextBoxOutletDto.Text = mArt.OutletDto
            TextBoxOutletQty.Text = mArt.OutletQty
        Else
            TextBoxOutletDto.Enabled = False
            TextBoxOutletQty.Enabled = False
        End If

        Xl_Art_Edit1.Art = mArt
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.Accounts
                If mArt.Id > 0 Then
                    ButtonDel.Enabled = True
                End If
            Case Else
                'TabControl1.TabPages.Remove(TabPageEdit)
                ButtonDel.Enabled = False
                ButtonOk.Enabled = False
        End Select

        If mMode = Modes.Edit Then
            TabControl1.SelectedIndex = Tabs.Edit
            ButtonOk.Enabled = True
        End If

        mAllowEvents = True
    End Sub

    Public ReadOnly Property Changed() As Boolean
        Get
            Return mChanged
        End Get
    End Property

    Private Sub SetGrupTabBackColor(ByVal BlGrup As Boolean)
        'Return

        If BlGrup Then
            TabPageGrup.ImageIndex = 0
        Else
            TabPageGrup.ImageIndex = -1
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub EnableButtons()
        mDirty = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim BlNewArt As Boolean = (mArt.Id = 0)
        If mDirty Then
            mArt = Xl_Art_Edit1.Art
            With mArt
                .Outlet = CheckBoxOutlet.Checked
                If .Outlet Then
                    .OutletDto = TextBoxOutletDto.Text
                    If IsNumeric(TextBoxOutletQty.Text) Then
                        .OutletQty = TextBoxOutletQty.Text
                    End If
                Else
                    .OutletDto = 0
                    .OutletQty = 0
                End If

                If Xl_ArtLogistics1.IsDirty Then
                    .Dimensions = Xl_ArtLogistics1.Dimensions
                    .Cnap = Xl_Art_Edit1.Xl_CnapLookup1.Cnap
                    .CodiMercancia = Xl_ArtLogistics1.CodiMercancia
                End If

                If mDirtyGroup Then
                    .ArtWiths = Xl_Art_Group1.Items
                End If
            End With
        End If

        Dim exs as New List(Of exception)
        If mArt.Update( exs) Then
            If BlNewArt Then MsgBox("nou article registrat:" & vbCrLf & mArt.Id & vbCrLf & mArt.Nom_ESP, MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(mArt, New System.EventArgs)
                Me.Close()
            Else
                MsgBox("error al desar l'article" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
    End Sub

    Private Sub Xl_Art_Edit1_AfterImageUpdate()
        Xl_Art_Gral1.Art = mArt
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sObs As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem article " & mArt.Nom_ESP & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mArt.Delete( exs) Then
                MsgBox("article eliminat", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(mArt, New System.EventArgs)
                Me.Close()
            Else
                MsgBox("error al eliminar article:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub



    Private Sub LoadTendencies()
        Dim SQL As String = "SELECT PDC.YEA, MONTH(PDC.FCH) AS MES, SUM(PNC.QTY) AS QTY " _
        & "FROM PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid=PDC.Guid " _
        & "WHERE Pdc.EMP=1 AND " _
        & "PNC.ArtGuid=@Guid AND " _
        & "PNC.COD=2 " _
        & "GROUP BY PDC.YEA, MONTH(PDC.FCH) " _
        & "ORDER BY PDC.YEA, MONTH(PDC.FCH)"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Guid", mArt.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        Dim iMax As Integer = 0
        For Each oRow In oTb.Rows
            If oRow("QTY") > iMax Then iMax = oRow("QTY")
        Next

        If oTb.Rows.Count > 0 Then

            Dim oFirstRow As DataRow = oTb.Rows(0)
            Dim iFirstYea As Integer = oFirstRow("YEA")
            Dim iFirstMes As Integer = oFirstRow("MES")

            Dim oLastRow As DataRow = oTb.Rows(oTb.Rows.Count - 1)
            Dim iLastYea As Integer = oLastRow("YEA")
            Dim iLastMes As Integer = oLastRow("MES")

            Dim iMesos As Integer
            If iFirstYea = iLastYea Then
                iMesos = iLastMes - iFirstMes + 1
            Else
                iMesos = (12 - iFirstMes + 1) + (12 * (iLastYea - iFirstYea)) + (iLastMes)
            End If

            Dim iItmWidth As Integer = 30
            Dim iWidth As Integer = iMesos * iItmWidth
            Dim iEpgHeight As Integer = 15
            Dim iBottomHeight As Integer = 15
            Dim iHeight As Integer = PictureBoxTendencies.Height - iEpgHeight - iBottomHeight
            Dim oImg As New System.Drawing.Bitmap(iWidth, PictureBoxTendencies.Height)
            Dim oGr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(oImg)
            Dim X1 As Integer
            Dim X2 As Integer = 0
            Dim Y As Integer


            Dim oFont As New Font("Arial", 8, FontStyle.Regular)
            Dim oLang As New DTOLang("CAT")
            Dim BlPen As Boolean
            Dim oBrush As Brush = Nothing
            Dim oBrushBlue As Brush = Brushes.Blue
            Dim iMes As Integer = iFirstMes
            Dim SngFactor As Decimal = iHeight / iMax
            Dim s As String
            Dim oEpgRc As Rectangle
            For Each oRow In oTb.Rows
                X1 = X2 + 1
                X2 = X1 + iItmWidth - 1
                BlPen = Not BlPen
                oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
                oEpgRc = New Rectangle(X1, 0, X2 - X1, iEpgHeight)
                s = oLang.MesAbr(oRow("MES"))
                oGr.FillRectangle(oBrush, X1, 0, X2 - X1, iEpgHeight)
                oGr.DrawString(s, oFont, Brushes.Black, X1, 0)

                BlPen = Not BlPen
                oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
                Y = iEpgHeight + iHeight - oRow("QTY") * SngFactor
                oGr.FillRectangle(oBrush, X1, iEpgHeight + 1, X2 - X1, Y - 1)
                oGr.FillRectangle(oBrushBlue, X1, Y, X2 - X1, iEpgHeight + iHeight)

                BlPen = Not BlPen
                oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
                s = Format(CDbl(oRow("QTY")), "#,##0")
                oGr.FillRectangle(oBrush, X1, iEpgHeight + iHeight + 1, X2 - X1, iBottomHeight)
                oGr.DrawString(s, oFont, Brushes.Black, X1, iEpgHeight + iHeight + 1)
            Next

            PictureBoxTendencies.Image = oImg
        End If
    End Sub



    Private Sub Xl_Art_Edit1_AfterImageUpdate1() Handles Xl_Art_Edit1.AfterImageUpdate
        RaiseEvent AfterUpdate(mArt, New System.EventArgs)
    End Sub

    Private Sub Xl_Art_Edit1_DscInheritanceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Art_Edit1.DscInheritanceChanged
        Dim sText As String = CStr(sender)
        Xl_Art_Gral1.SetDsc(sText)
    End Sub

    Private Sub Xl_Art_Edit1_ForzarInnerPackChanged(ByVal BlForzar As Boolean, ByVal iInnerPack As Integer) Handles Xl_Art_Edit1.ForzarInnerPackChanged
        Xl_Art_Gral1.forzarInnerPackChanged(BlForzar, iInnerPack)
    End Sub

    Private Sub Xl_Art_Group1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Art_Group1.AfterUpdate
        mDirtyGroup = True
        ButtonOk.Enabled = True

        Dim BlDirty As Boolean = (Xl_Art_Group1.GroupParentOnly <> mArt.GroupParentOnly)
        If BlDirty Then
            SetGrupTabBackColor(Xl_Art_Group1.Items.Count > 0)
            mArt.GroupParentOnly = Xl_Art_Group1.GroupParentOnly
            mDirty = True
        End If
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Tendencia
                    LoadTendencies()
                Case Tabs.Logistics
                    Xl_ArtLogistics1.Target = mArt
                Case Tabs.Grup
                    Xl_Art_Group1.Art = mArt
                Case Tabs.Accessories
                    LoadAccessories()
                Case Tabs.Spares
                    LoadSpares()
                Case Tabs.Downloads
                    LoadProductDownloads()
                Case Tabs.Galeria

            End Select
            CleanTab(iTab) = True
        End If
    End Sub

    Private Sub LoadAccessories()
        Xl_ArtsAccessories.SetAccessoriesFrom(New Product(mArt))
    End Sub

    Private Sub LoadSpares()
        Xl_ArtsSpares.SetSparesFromStp(New Product(mArt))
    End Sub

    Protected Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxOutletDto.TextChanged, TextBoxOutletQty.TextChanged, Xl_ArtLogistics1.AfterUpdate

        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub CheckBoxOutlet_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOutlet.CheckedChanged
        If mAllowEvents Then
            TextBoxOutletDto.Enabled = CheckBoxOutlet.Checked
            TextBoxOutletQty.Enabled = CheckBoxOutlet.Checked
            EnableButtons()
        End If
    End Sub

    Private Sub TabControl1_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = Tabs.Edit Then
            Select Case BLL.BLLSession.Current.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                Case Else
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oProduct As New DTOProductSku(mArt.Guid)
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct
        oProductDownload.DocFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProduct As New DTOProductSku(mArt.Guid)
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        LoadProductDownloads()
    End Sub

    Private Sub LoadProductDownloads()
        Dim oProduct As New DTOProductSku(mArt.Guid)
        Dim oDownloads As List(Of DTOProductDownload) = BLL.BLLProductDownloads.FromProductOrParent(oProduct)
        Xl_ProductDownloads1.Load(oDownloads)
    End Sub
End Class