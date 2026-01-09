import { Component } from '@angular/core';
import { Episodes } from '../../interfaces/episodes';
import { ApiServices } from '../../services/api-services';
import { FormControl } from '@angular/forms';

import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-api',
  standalone: true, //componente independiente - permite usar el componente sin necesidad de importarlo en el app.module
  imports: [ReactiveFormsModule], //importacion de ReactiveFormsModule
  templateUrl: './api.html',
  styleUrl: './api.css',
})
export class Api {
  searchControl = new FormControl('');
  constructor(private apiServices: ApiServices,
  ) {

  }
  listEpisodes: Episodes[] = [];
  filterEpi: Episodes[] = [];
  ngOnInit(): void {
    this.apiServices.getEpisodes().subscribe({    //funcion para obtener los episodios desde la api 
      next: (data) => {
        this.filterEpi = data;
        this.listEpisodes = data;

      },

      error: (err) => console.error('Error al conectar con la API', err)
    });
    this.searchControl.valueChanges.subscribe(value => { //funcion para filtrar los episodios
      this.filterEpisodes(String(value));
    });
  }
  filterEpisodes(query: string) {    //funcion para filtrar los episodios
    const cleanQuery = query.toLowerCase();
    this.filterEpi = this.listEpisodes.filter((episode) => {
      return (
        episode.name.toLowerCase().includes(cleanQuery) ||
        episode.id.toString().toLowerCase().includes(cleanQuery) ||
        episode.airDate.toLowerCase().includes(cleanQuery) ||
        episode.episodeCode.toLowerCase().includes(cleanQuery) ||
        episode.characters.some((charUrl) => charUrl.toLowerCase().includes(cleanQuery)) ||
        episode.characterNames.some((charUrl) => charUrl.toLowerCase().includes(cleanQuery)) ||
        episode.created.toLowerCase().includes(cleanQuery)
      );
    }); console.log('Episodios obtenidos correctamente', this.listEpisodes)
  }
}
