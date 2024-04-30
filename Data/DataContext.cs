namespace mvc;

using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public class DataContext: IDataContext{

    private readonly MySqlConnection _connection;
    public DataContext(MySqlConnection connection){
        _connection = connection;
    }
    public async Task<List<Producto>> obtenerProductosAsync()
    {
        await _connection.OpenAsync();
        List<Producto> productos= new ();
        using var command = new MySqlCommand(@"SELECT producto.id, producto.nombre, 
        producto.precio, fabricante.nombre AS fabricante_nombre 
        FROM fabricante INNER JOIN producto ON fabricante.id = producto.id_fabricante", _connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()){
            Producto item = new(){
                ProductoId = Convert.ToInt32(reader["id"]),
                Nombre = reader["nombre"].ToString(),  
                Precio = Convert.ToDecimal(reader["precio"]),
                Fabricante = reader["fabricante_nombre"].ToString(),
            };
            productos.Add(item);   
        }
        return productos;
    }

    
}