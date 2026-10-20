import { useState } from 'react';

//explicacion de que hace el codigo
/*
    Este codigo es un hook personalizado de react que se encarga de manejar el estado de carga de una aplicacion.
    La funcion withLoading es una funcion que envuelve cualquier funcion asincrona y se encarga de prender el loading antes de ejecutar la funcion y apagarlo despues de ejecutar la funcion.
*/


export const useAsyncState = (initialState: boolean = false) => {
  const [isLoading, setIsLoading] = useState(initialState);
  const [error, setError] = useState<string | null>(null); // Añadimos el estado del error aquí

  // Esta "envoltura mágica" ahora prende el loading, ejecuta tu función, 
  // maneja el error internamente si falla, y luego apaga el loading.
  const withAsync = async <T,>(asyncFunction: () => Promise<T>) => {
    setIsLoading(true);
    setError(null); // Limpiamos errores anteriores antes de cada petición

    try {
      const data = await asyncFunction();
      return { data, hasError: false }; // Retorna los datos si es exitoso
    } catch (err: any) {
      let errorMessage = 'Ocurrió un error inesperado al conectar con el servidor.'
      if (err.response && err.response.data) {
        errorMessage = err.response.data.message || err.response.data.title || JSON.stringify(err.response.data);
      } else if (err.message) {
        errorMessage = err.message;
      }
      setError(errorMessage);
      return { data: null, hasError: true, errorMessage };
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, withAsync, setError };
};

// <T,> es una convencion de typescript para indicar que es un tipo generico
// la , en <T,> es para indicar que es un tipo generico, y que React no lo confunda con una etiqueta html

//porque hacer  
/*
withLoading(() => fakeApiCall()) en vez de 
withLoading(fakeApiCall())

porque si se llama directamente a la funcion se ejecuta de imediato, es decir no da tiempo al estado de loading de funcionar
() => fakeApiCall() esto es pasarle la funcion directamente, entonces el hook la ejecuta cuando debe

*/