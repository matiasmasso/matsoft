

Public Class Xl_Bancs_Select
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(48, 48)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(150, 90)
        Me.ListView1.TabIndex = 1
        '
        'Xl_Bancs_Select
        '
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Xl_Bancs_Select"
        Me.Size = New System.Drawing.Size(150, 90)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mBancs As List(Of DTOBanc)
    Private mAllowEvents As Boolean
    Private mMenuItemObsoletos As New ToolStripMenuItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Banc() As DTOBanc
        Get
            Return CurrentBanc()
        End Get
        Set(ByVal Value As DTOBanc)
            Dim i As Integer
            If Not Value Is Nothing Then

                LoadBancs()

                ListView1.SelectedIndices.Clear()
                For i = 0 To mBancs.Count - 1
                    If Value.Id = mBancs(i).Id Then
                        ListView1.SelectedIndices.Add(i)
                        Exit For
                    End If
                Next
            End If
        End Set
    End Property

    Private Sub LoadBancs()
        If mBancs Is Nothing Then
            mBancs = BLL.BLLBancs.All()
            If mBancs.Count > 0 Then
                refresca()
                mAllowEvents = True
            End If
        End If
    End Sub

    Private Sub Xl_Bancs_Select_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBancs()
    End Sub

    Private Sub refresca()
        ListView1.Items.Clear()
        Dim oBanc As DTOBanc
        Dim oLv As ListViewItem
        Dim Idx As Integer
        For Each oBanc In mBancs
            If mMenuItemObsoletos.Checked Or (Not oBanc.Obsoleto) Then

                ImageList1.Images.Add(oBanc.Iban.BankBranch.Bank.Logo)
                oLv = New ListViewItem
                With oLv
                    .Text = oBanc.Abr
                    .ImageIndex = Idx
                End With
                ListView1.Items.Add(oLv)
                Idx = Idx + 1
            End If
        Next
    End Sub

    Private Function CurrentBanc() As DTOBanc
        Dim oBanc As DTOBanc = Nothing
        If ListView1.SelectedIndices.Count > 0 Then
            oBanc = mBancs(ListView1.SelectedIndices(0))
        End If
        Return oBanc
    End Function

    Private Sub MenuItemObsolets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mMenuItemObsoletos.Checked = Not mMenuItemObsoletos.Checked
        refresca()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedIndices.Count > 0 Then
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If mAllowEvents Then
            RaiseEvent AfterUpdate(sender, e)
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If ListView1.SelectedItems.Count = 1 Then
            Dim oMenu_Banc As New Menu_Banc(CurrentBanc)
            oContextMenu.Items.AddRange(oMenu_Banc.Range)
        End If

        oContextMenu.Items.Add(mMenuItemObsoletos)

        ListView1.ContextMenuStrip = oContextMenu
    End Sub

End Class

