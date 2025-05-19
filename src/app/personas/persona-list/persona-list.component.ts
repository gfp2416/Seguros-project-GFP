import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonaService } from '../persona.service';
import { Persona } from '../persona';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-persona-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './persona-list.component.html'
})
export class PersonaListComponent implements OnInit {
  private personaService = inject(PersonaService);
  private authService = inject(AuthService);
  personas: Persona[] = [];
  usuario: any;

  ngOnInit(): void {
    const storedUser = localStorage.getItem('usuario');
  if (storedUser) {
    this.usuario = storedUser;
  }
    this.personaService.getAll().subscribe(data => {
      this.personas = data;
    });
  }

  eliminar(id: string) {
    if (confirm('¿Está seguro de eliminar esta persona?')) {
      this.personaService.delete(id).subscribe(() => {
        this.personas = this.personas.filter(p => p.id !== id);
      });
    }
  }

logout(){
  this.authService.logout();
}

}
