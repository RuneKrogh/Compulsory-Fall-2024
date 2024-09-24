// src/atoms.ts
import { atom } from 'jotai';
import { Customer } from '../Api.ts'; // Adjust the import based on your structure

export const customersAtom = atom<Customer[]>([]);
