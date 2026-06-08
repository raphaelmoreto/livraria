import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspecionarLivroComponent } from './inspecionar-livro.component';

describe('InspecionarLivroComponent', () => {
  let component: InspecionarLivroComponent;
  let fixture: ComponentFixture<InspecionarLivroComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InspecionarLivroComponent]
    });
    fixture = TestBed.createComponent(InspecionarLivroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
