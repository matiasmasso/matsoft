Public Class Xl_Art_Menu
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

    Private mArt As MaxiSrvr.Art

    Public WriteOnly Property Art() As MaxiSrvr.Art
        Set(ByVal Value As MaxiSrvr.Art)
            mArt = Value
            With MyBase.MenuItems
                .Clear()
                .Add("Zoom", New System.EventHandler(AddressOf Zoom))
                ' .Add("Atlas", New System.EventHandler(AddressOf Atlas))
                .Add("Previsió", New System.EventHandler(AddressOf Previsio))
            End With
        End Set
    End Property

    Private Sub Zoom(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowArt(mArt)
    End Sub

    Private Sub Previsio(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowArtPrevisions(mArt)
    End Sub

  

End Class

