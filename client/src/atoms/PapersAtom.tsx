import { atom } from 'jotai';
import {Paper} from '../Api.ts';

export const papersAtom = atom<Paper[]>([]);
