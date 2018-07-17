import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersaveComponent } from './usersave.component';

describe('UsersaveComponent', () => {
  let component: UsersaveComponent;
  let fixture: ComponentFixture<UsersaveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersaveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
