

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
    Friend WithEvents TabPageAccessoris As System.Windows.Forms.TabPage
    Friend WithEvents TabPageLogistica As System.Windows.Forms.TabPage
    Friend WithEvents Xl_ArtLogistics1 As Xl_Art_Logistics
    Friend WithEvents TabPageEdit As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Art_Edit1 As Xl_Art_Edit
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Art_Gral1 As Xl_Art_Gral
    Friend WithEvents TabPageRecursos As TabPage
    Friend WithEvents Xl_MediaResources1 As Xl_MediaResources
    Friend WithEvents Xl_ProductSkusExtendedSpares As Xl_ProductSkusExtended
    Friend WithEvents Xl_ProductSkusExtendedAccessories As Xl_ProductSkusExtended
    Friend WithEvents ProgressBarMediaResources As ProgressBar
    Friend WithEvents TabPageFiltres As TabPage
    Friend WithEvents Xl_CheckedFilters1 As Xl_CheckedFilters
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Art))
        Me.ImageListTabs = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabPageDownloads = New System.Windows.Forms.TabPage()
        Me.Xl_ProductDownloads1 = New Winforms.Xl_ProductDownloads()
        Me.TabPageOutlet = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxOutletQty = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxOutletDto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBoxOutlet = New System.Windows.Forms.CheckBox()
        Me.TabPageSpares = New System.Windows.Forms.TabPage()
        Me.Xl_ProductSkusExtendedSpares = New Winforms.Xl_ProductSkusExtended()
        Me.TabPageAccessoris = New System.Windows.Forms.TabPage()
        Me.Xl_ProductSkusExtendedAccessories = New Winforms.Xl_ProductSkusExtended()
        Me.TabPageLogistica = New System.Windows.Forms.TabPage()
        Me.Xl_ArtLogistics1 = New Winforms.Xl_Art_Logistics()
        Me.TabPageEdit = New System.Windows.Forms.TabPage()
        Me.Xl_Art_Edit1 = New Winforms.Xl_Art_Edit()
        Me.TabPageGral = New System.Windows.Forms.TabPage()
        Me.Xl_Art_Gral1 = New Winforms.Xl_Art_Gral()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageRecursos = New System.Windows.Forms.TabPage()
        Me.ProgressBarMediaResources = New System.Windows.Forms.ProgressBar()
        Me.Xl_MediaResources1 = New Winforms.Xl_MediaResources()
        Me.TabPageFiltres = New System.Windows.Forms.TabPage()
        Me.Xl_CheckedFilters1 = New Winforms.Xl_CheckedFilters()
        Me.Panel1.SuspendLayout()
        Me.TabPageDownloads.SuspendLayout()
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageOutlet.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageSpares.SuspendLayout()
        CType(Me.Xl_ProductSkusExtendedSpares, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageAccessoris.SuspendLayout()
        CType(Me.Xl_ProductSkusExtendedAccessories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageLogistica.SuspendLayout()
        Me.TabPageEdit.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageRecursos.SuspendLayout()
        Me.TabPageFiltres.SuspendLayout()
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
        Me.TabPageDownloads.Text = "Descàrregues"
        Me.TabPageDownloads.UseVisualStyleBackColor = True
        '
        'Xl_ProductDownloads1
        '
        Me.Xl_ProductDownloads1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductDownloads1.Filter = Nothing
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
        Me.TabPageOutlet.Text = "Outlet"
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(158, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "unitats"
        '
        'TextBoxOutletQty
        '
        Me.TextBoxOutletQty.Location = New System.Drawing.Point(119, 72)
        Me.TextBoxOutletQty.Name = "TextBoxOutletQty"
        Me.TextBoxOutletQty.Size = New System.Drawing.Size(33, 20)
        Me.TextBoxOutletQty.TabIndex = 5
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(155, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "%"
        '
        'TextBoxOutletDto
        '
        Me.TextBoxOutletDto.Location = New System.Drawing.Point(119, 46)
        Me.TextBoxOutletDto.Name = "TextBoxOutletDto"
        Me.TextBoxOutletDto.Size = New System.Drawing.Size(33, 20)
        Me.TextBoxOutletDto.TabIndex = 2
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
        'TabPageSpares
        '
        Me.TabPageSpares.Controls.Add(Me.Xl_ProductSkusExtendedSpares)
        Me.TabPageSpares.Location = New System.Drawing.Point(4, 23)
        Me.TabPageSpares.Name = "TabPageSpares"
        Me.TabPageSpares.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSpares.Size = New System.Drawing.Size(734, 455)
        Me.TabPageSpares.TabIndex = 7
        Me.TabPageSpares.Text = "Recanvis"
        Me.TabPageSpares.UseVisualStyleBackColor = True
        '
        'Xl_ProductSkusExtendedSpares
        '
        Me.Xl_ProductSkusExtendedSpares.AllowUserToAddRows = False
        Me.Xl_ProductSkusExtendedSpares.AllowUserToDeleteRows = False
        Me.Xl_ProductSkusExtendedSpares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkusExtendedSpares.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkusExtendedSpares.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductSkusExtendedSpares.Name = "Xl_ProductSkusExtendedSpares"
        Me.Xl_ProductSkusExtendedSpares.ReadOnly = True
        Me.Xl_ProductSkusExtendedSpares.ShowObsolets = False
        Me.Xl_ProductSkusExtendedSpares.Size = New System.Drawing.Size(728, 449)
        Me.Xl_ProductSkusExtendedSpares.TabIndex = 1
        '
        'TabPageAccessoris
        '
        Me.TabPageAccessoris.Controls.Add(Me.Xl_ProductSkusExtendedAccessories)
        Me.TabPageAccessoris.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAccessoris.Name = "TabPageAccessoris"
        Me.TabPageAccessoris.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAccessoris.Size = New System.Drawing.Size(734, 455)
        Me.TabPageAccessoris.TabIndex = 6
        Me.TabPageAccessoris.Text = "Accessoris"
        Me.TabPageAccessoris.UseVisualStyleBackColor = True
        '
        'Xl_ProductSkusExtendedAccessories
        '
        Me.Xl_ProductSkusExtendedAccessories.AllowUserToAddRows = False
        Me.Xl_ProductSkusExtendedAccessories.AllowUserToDeleteRows = False
        Me.Xl_ProductSkusExtendedAccessories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductSkusExtendedAccessories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductSkusExtendedAccessories.Location = New System.Drawing.Point(3, 3)
        Me.Xl_ProductSkusExtendedAccessories.Name = "Xl_ProductSkusExtendedAccessories"
        Me.Xl_ProductSkusExtendedAccessories.ReadOnly = True
        Me.Xl_ProductSkusExtendedAccessories.ShowObsolets = False
        Me.Xl_ProductSkusExtendedAccessories.Size = New System.Drawing.Size(728, 449)
        Me.Xl_ProductSkusExtendedAccessories.TabIndex = 0
        '
        'TabPageLogistica
        '
        Me.TabPageLogistica.Controls.Add(Me.Xl_ArtLogistics1)
        Me.TabPageLogistica.Location = New System.Drawing.Point(4, 23)
        Me.TabPageLogistica.Name = "TabPageLogistica"
        Me.TabPageLogistica.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLogistica.Size = New System.Drawing.Size(734, 455)
        Me.TabPageLogistica.TabIndex = 10
        Me.TabPageLogistica.Text = "Logística"
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
        Me.TabPageEdit.Text = "Editar"
        Me.TabPageEdit.UseVisualStyleBackColor = True
        '
        'Xl_Art_Edit1
        '
        Me.Xl_Art_Edit1.Dirty = False
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
        Me.TabPageGral.Text = "General"
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
        Me.TabControl1.Controls.Add(Me.TabPageAccessoris)
        Me.TabControl1.Controls.Add(Me.TabPageSpares)
        Me.TabControl1.Controls.Add(Me.TabPageOutlet)
        Me.TabControl1.Controls.Add(Me.TabPageDownloads)
        Me.TabControl1.Controls.Add(Me.TabPageRecursos)
        Me.TabControl1.Controls.Add(Me.TabPageFiltres)
        Me.TabControl1.ImageList = Me.ImageListTabs
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(742, 482)
        Me.TabControl1.TabIndex = 18
        '
        'TabPageRecursos
        '
        Me.TabPageRecursos.Controls.Add(Me.ProgressBarMediaResources)
        Me.TabPageRecursos.Controls.Add(Me.Xl_MediaResources1)
        Me.TabPageRecursos.Location = New System.Drawing.Point(4, 23)
        Me.TabPageRecursos.Name = "TabPageRecursos"
        Me.TabPageRecursos.Size = New System.Drawing.Size(734, 455)
        Me.TabPageRecursos.TabIndex = 11
        Me.TabPageRecursos.Text = "Recursos"
        Me.TabPageRecursos.UseVisualStyleBackColor = True
        '
        'ProgressBarMediaResources
        '
        Me.ProgressBarMediaResources.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBarMediaResources.Location = New System.Drawing.Point(0, 432)
        Me.ProgressBarMediaResources.Name = "ProgressBarMediaResources"
        Me.ProgressBarMediaResources.Size = New System.Drawing.Size(734, 23)
        Me.ProgressBarMediaResources.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBarMediaResources.TabIndex = 1
        '
        'Xl_MediaResources1
        '
        Me.Xl_MediaResources1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_MediaResources1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_MediaResources1.Name = "Xl_MediaResources1"
        Me.Xl_MediaResources1.Size = New System.Drawing.Size(734, 455)
        Me.Xl_MediaResources1.TabIndex = 0
        '
        'TabPageFiltres
        '
        Me.TabPageFiltres.Controls.Add(Me.Xl_CheckedFilters1)
        Me.TabPageFiltres.Location = New System.Drawing.Point(4, 23)
        Me.TabPageFiltres.Name = "TabPageFiltres"
        Me.TabPageFiltres.Size = New System.Drawing.Size(734, 455)
        Me.TabPageFiltres.TabIndex = 12
        Me.TabPageFiltres.Text = "Filtres"
        Me.TabPageFiltres.UseVisualStyleBackColor = True
        '
        'Xl_CheckedFilters1
        '
        Me.Xl_CheckedFilters1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CheckedFilters1.IgnoreClickAction = 0
        Me.Xl_CheckedFilters1.isDirty = False
        Me.Xl_CheckedFilters1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CheckedFilters1.Name = "Xl_CheckedFilters1"
        Me.Xl_CheckedFilters1.Size = New System.Drawing.Size(734, 455)
        Me.Xl_CheckedFilters1.TabIndex = 1
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
        CType(Me.Xl_ProductDownloads1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageOutlet.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPageSpares.ResumeLayout(False)
        CType(Me.Xl_ProductSkusExtendedSpares, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageAccessoris.ResumeLayout(False)
        CType(Me.Xl_ProductSkusExtendedAccessories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageLogistica.ResumeLayout(False)
        Me.TabPageEdit.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageRecursos.ResumeLayout(False)
        Me.TabPageFiltres.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Private mArt As Art
    Private _Sku As DTOProductSku
    Private mCancel As Boolean
    Private mChanged As Boolean
    Private mAllowEvents As Boolean
    Private mAllowEventsDownload As Boolean
    Private mDirty As Boolean
    Private mDirtyGroup As Boolean
    Private mDirtyStream As Boolean
    Private mMode As Modes
    Private _FiltersLoaded As Boolean
    Private CleanTab(20) As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Enum Modes
        Browse
        Edit
    End Enum


    Private Enum Tabs
        General
        Edit
        Logistics
        Accessories
        Spares
        Outlet
        Downloads
        MediaResources
        Filters
    End Enum

    Private Enum ColsDownloads
        Guid
        Src
        Img
        Text
        Status
    End Enum


    Public Sub New(ByVal oProductSku As DTOProductSku, Optional oMode As Modes = Modes.Browse)
        MyBase.New()
        InitializeComponent()
        _Sku = oProductSku
        mMode = oMode
    End Sub

    Private Async Sub Frm_Art_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub


    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        If _Sku.IsNew Then
            Me.Text = "alta de nou article"
        Else
            If FEB2.ProductSku.Load(_Sku, exs, IncludeImage:=True, oMgz:=GlobalVariables.Emp.Mgz) Then
                If _Sku.isBundle Then
                    Dim oFrm As New Frm_SkuBundle(_Sku)
                    oFrm.Show()
                    Me.Close()
                Else
                    Me.Text = String.Format("article {0} {1}", _Sku.id, _Sku.nomLlarg.Tradueix(Current.Session.Lang))
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        Await Xl_Art_Gral1.Load(_Sku)

        CheckBoxOutlet.Checked = _Sku.Outlet
        If _Sku.Outlet Then
            TextBoxOutletDto.Text = _Sku.OutletDto
            TextBoxOutletQty.Text = _Sku.OutletQty
        Else
            TextBoxOutletDto.Enabled = False
            TextBoxOutletQty.Enabled = False
        End If

        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
                ButtonDel.Enabled = Not _Sku.IsNew
            Case Else
                'TabControl1.TabPages.Remove(TabPageEdit)
                ButtonDel.Enabled = False
                ButtonOk.Enabled = False
        End Select

        If mMode = Modes.Edit Then
            'Xl_Art_Edit1.Art = mArt
            TabControl1.SelectedIndex = Tabs.Edit
            ButtonOk.Enabled = True
        End If

        mAllowEvents = True
    End Function

    Public ReadOnly Property Changed() As Boolean
        Get
            Return mChanged
        End Get
    End Property


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub EnableButtons()
        mDirty = True
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_Art_Edit1.Dirty Then
            Xl_Art_Edit1.Update(_Sku)
        End If

        If _Sku.IvaCod = DTOTax.Codis.Exempt Then
            UIHelper.WarnError("falta especificar modalitat de IVA")
        Else
            If mDirty Then
                With _Sku
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
                        .HeredaDimensions = Xl_ArtLogistics1.Dimensions.Hereda
                        .DimensionL = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.DimensionLargo)
                        .DimensionW = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.DimensionAncho)
                        .DimensionH = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.DimensionAlto)
                        .NoDimensions = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.NoDimensions)
                        .CodiMercancia = Xl_ArtLogistics1.Dimensions.CodiMercancia
                        .ForzarInnerPack = IIf(.HeredaDimensions, False, Xl_ArtLogistics1.Dimensions.ForzarInnerPack)
                        .InnerPack = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.InnerPack)
                        .OuterPack = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.OuterPack)
                        .KgBrut = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.KgBrut)
                        .KgNet = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.KgNet)
                        .VolumeM3 = IIf(.HeredaDimensions, 0, Xl_ArtLogistics1.Dimensions.M3)
                    End If

                    .Cnap = Xl_Art_Edit1.Xl_LookupCnap1.Cnap

                End With
            End If

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            Dim isNew As Boolean = _Sku.IsNew
            If Await FEB2.ProductSku.Update(_Sku, exs) Then
                If Xl_CheckedFilters1.isDirty Then
                    If Await FEB2.FilterTargets.Update(exs, _Sku, Xl_CheckedFilters1.SelectedValues) Then
                        Xl_CheckedFilters1.isDirty = False
                    End If
                End If

                If isNew Then MsgBox("nou article registrat:" & vbCrLf & _Sku.id & vbCrLf & _Sku.nomLlarg.Tradueix(Current.Session.Lang), MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Sku))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar l'article")
            End If


        End If


    End Sub



    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sObs As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem article " & _Sku.nomLlarg.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ProductSku.Delete(_Sku, exs) Then
                MsgBox("article eliminat", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Sku))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar article:")
            End If
        End If
    End Sub




    Private Sub Xl_Art_Edit1_DscInheritanceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Art_Edit1.DscInheritanceChanged
        Dim sText As String = CStr(sender)
        Xl_Art_Gral1.SetDsc(sText)
    End Sub

    Private Sub Xl_Art_Edit1_ForzarInnerPackChanged(ByVal BlForzar As Boolean, ByVal iInnerPack As Integer) Handles Xl_Art_Edit1.ForzarInnerPackChanged
        Xl_Art_Gral1.forzarInnerPackChanged(BlForzar, iInnerPack)
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Edit
                    If Not Await Xl_Art_Edit1.Load(exs, _Sku) Then
                        UIHelper.WarnError(exs)
                    End If
                Case Tabs.Logistics
                    Xl_ArtLogistics1.Target = _Sku
                Case Tabs.Accessories
                    Await LoadAccessories()
                Case Tabs.Spares
                    Await LoadSpares()
                Case Tabs.Downloads
                    LoadProductDownloads()
                Case Tabs.MediaResources
                    Await LoadMediaResources()
                Case Tabs.Filters
                    Await LoadFilters()
            End Select
            CleanTab(iTab) = True
        End If
    End Sub

    Private Async Function LoadAccessories() As Task
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Sku, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedAccessories.Load(oAccessories,,, DTOProduct.Relateds.Accessories)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadSpares() As Task
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Sku, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedSpares.Load(oSpares,,, DTOProduct.Relateds.Spares)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadMediaResources() As Task
        Dim exs As New List(Of Exception)
        ProgressBarMediaResources.Visible = True
        Dim items = Await FEB2.MediaResources.AllWithThumbnails(exs, _Sku)
        ProgressBarMediaResources.Visible = False
        If exs.Count = 0 Then
            Xl_MediaResources1.Load(_Sku, items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

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
            Select Case Current.Session.User.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.accounts
                Case Else
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Target = _Sku
        oProductDownload.DocFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oProductDownload = DTOProductDownload.Factory(_Sku, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        LoadProductDownloads()
    End Sub

    Private Async Sub LoadProductDownloads()
        Dim exs As New List(Of Exception)
        Dim oDownloads As List(Of DTOProductDownload) = Await FEB2.Downloads.FromProductOrParent(exs, _Sku)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadFilters() As Task
        Dim exs As New List(Of Exception)
        If Not _FiltersLoaded Then
            Dim oAllFilters = Await FEB2.Filters.All(exs)
            If exs.Count = 0 Then
                Dim oCheckedItems = Await FEB2.FilterTargets.All(exs, _Sku)
                If exs.Count = 0 Then
                    Xl_CheckedFilters1.Load(oAllFilters, oCheckedItems)
                    _FiltersLoaded = True
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Async Sub Xl_MediaResources1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MediaResources1.RequestToRefresh
        Await LoadMediaResources()
    End Sub

    Private Sub Xl_ProductSkusExtendedAccessories_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToAddNewSku
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku)
        AddHandler oFrm.onItemSelected, AddressOf addAccessory
        oFrm.Show()
    End Sub

    Private Async Sub AddAccessory(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Sku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                MsgBox(oAccessory.nomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a accessori de " & _Sku.nomLlarg.Tradueix(Current.Session.Lang))
            Else
                oAccessories.Add(e.Argument)
                If Await FEB2.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _Sku, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToAddNewSku
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Sku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                MsgBox(oSpare.nomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a recanvi de " & _Sku.nomLlarg.Tradueix(Current.Session.Lang))
            Else
                oSpares.Add(e.Argument)
                If Await FEB2.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _Sku, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedAccessories_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Sku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                oAccessories.RemoveAll(Function(x) x.Equals(oAccessory))
                If Await FEB2.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _Sku, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oAccessory.nomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a accessori de " & _Sku.nomLlarg.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Sku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                oSpares.RemoveAll(Function(x) x.Equals(oSpare))
                If Await FEB2.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _Sku, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oSpare.nomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a recanvi de " & _Sku.nomLlarg.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Art_Edit1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Art_Edit1.AfterUpdate
        EnableButtons()
    End Sub

    Private Sub Xl_Art_Edit1_AfterImageUpdate1(sender As Object, e As MatEventArgs) Handles Xl_Art_Edit1.AfterImageUpdate
        Xl_Art_Gral1.PictureBoxImg.Image = e.Argument
        EnableButtons()
    End Sub

    Private Sub Xl_CheckedFilters1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CheckedFilters1.AfterUpdate
        EnableButtons()
    End Sub

    Private Sub Xl_Art_Edit1_RequestToRefreshLangTexts(sender As Object, e As MatEventArgs) Handles Xl_Art_Edit1.RequestToRefreshLangTexts
        Dim oSku As DTOProductSku = e.Argument
        Xl_Art_Gral1.LabelNom.Text = oSku.NomLlarg.Tradueix(Current.Session.Lang)
        Xl_Art_Gral1.LabelNomCurt.Text = oSku.Nom.Tradueix(Current.Session.Lang)
    End Sub

    Private Sub Xl_Art_Edit1_AvailabilityChanged(sender As Object, e As MatEventArgs) Handles Xl_Art_Edit1.AvailabilityChanged
        Xl_Art_Gral1.UpdateAvailability(e.Argument)
    End Sub
End Class