Public Class Xl_Csa_Menu
    Inherits System.Windows.Forms.ContextMenu

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Private mCsa As MaxiSrvr.Csa

    Public WriteOnly Property Csa() As MaxiSrvr.Csa
        Set(ByVal Value As MaxiSrvr.Csa)
            mCsa = Value
            With MyBase.MenuItems
                .Clear()
                .Add("Zoom", New System.EventHandler(AddressOf Zoom))
                .Add("Fitxer", New System.EventHandler(AddressOf DoFile))
                .Add("Imprimeix", New System.EventHandler(AddressOf Print))
                .Add("Graba despeses", New System.EventHandler(AddressOf Despeses))
                'Dim oMnuClient As MenuItem = .Add("Client...")
                'With oMnuClient.MenuItems
                '.Add("Ultimes comandes", New System.EventHandler(AddressOf CliPdcs))
                'End With
            End With
        End Set
    End Property

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCsa(mCsa)
    End Sub

    Private Sub DoFile(ByVal sender As Object, ByVal e As System.EventArgs)
        root.SaveCsaFile(mCsa)
    End Sub

    Private Sub Print(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsas As New MaxiSrvr.Csas
        oCsas.Add(mCsa)
        root.PrintCsas(oCsas, maxisrvr.ReportDocument.PrintModes.Copia)
    End Sub

    Private Sub Despeses(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        Dim oAmt As MaxiSrvr.Amt = mCsa.SetDespeses_AEB19(root.Usuari, exs)
        If oAmt Is Nothing Then
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        Else
            MsgBox("despeses: " & oAmt.CurFormat)
        End If
    End Sub
End Class
