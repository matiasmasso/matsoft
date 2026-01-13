

Public Class Frm_Art_Select
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
    Friend WithEvents HelpProviderHG As HelpProvider

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox1, "Frm_Art_Select.htm#TextBox1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox1, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.HelpProviderHG.SetShowHelp(Me.TextBox1, True)
        Me.TextBox1.Size = New System.Drawing.Size(504, 20)
        Me.TextBox1.TabIndex = 0
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'Frm_Art_Select
        '
        Me.ClientSize = New System.Drawing.Size(504, 22)
        Me.Controls.Add(Me.TextBox1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "Frm_Art_Select.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "Frm_Art_Select"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "ARTICLE"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Sku As DTOProductSku

    Private Async Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Dim exs As New List(Of Exception)
        If e.KeyCode = Keys.Enter Then
            Dim sKey As String = TextBox1.Text
            If Not _Sku Is Nothing Then
                If sKey = _Sku.nomLlarg.Tradueix(Current.Session.Lang) Then
                    Dim oFrm As New Frm_Art(_Sku)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                    Exit Sub
                End If
            End If

            If sKey = "" Then
                _Sku = Nothing
            Else
                Dim oMgz As DTOMgz = GlobalVariables.Emp.Mgz
                _Sku = Await Finder.FindSku(exs, Current.Session.Emp, sKey) ', oMgz) si no posem el magatzem no perdrà el temps amb els stocks
                If exs.Count = 0 Then
                    RefreshRequest(Me, New MatEventArgs(_Sku))
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        _Sku = e.Argument
        If _Sku IsNot Nothing Then
            TextBox1.Text = _Sku.nomLlarg.Tradueix(Current.Session.Lang)
            Me.Text = "ARTICLE " & _Sku.Id
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Sku IsNot Nothing Then
            Dim oMenu_Sku As New Menu_ProductSku(_Sku)
            AddHandler oMenu_Sku.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Sku.Range)

            oContextMenu.Items.Add("-")
        End If

        TextBox1.ContextMenuStrip = oContextMenu
    End Sub


End Class
