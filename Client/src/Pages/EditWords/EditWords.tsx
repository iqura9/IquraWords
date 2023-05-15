import "./style.css";
import TrashIcon from "./../../assets/trash.svg";
import GalleryIcon from "./../../assets/gallery.svg";
import WordIcon from "./../../assets/word.svg";
import BookIcon from "./../../assets/book.svg";
import MagicpenIcon from "./../../assets/magicpen.svg";
import DocumentIcon from "./../../assets/document-text.svg";
import {FC, useEffect, useRef, useState} from "react";


export const EditWords = () => {
    const arr = [1, 2, 3, 4];
    const HandleSubmit = () => {
        console.log("submit");
    };
    return (
        <div className="edit-words__wrapper">
            <div className="edit-words__header">Library {">"} Courses {">"} Слова ЗНО англ мова</div>
            <div className="edit-words__create-a-cluster">Create a cluster</div>
            <div className="edit-words__form-group">
                <div className="edit-words__form-group-img">
                    <MagicpenIcon/>
                </div>
                <input className="form-group__input" placeholder="Label"></input>
            </div>
            <div className="edit-words__form-group">
                <div className="edit-words__form-group-img">
                    <DocumentIcon/>
                </div>
                <input className="form-group__input" placeholder="Description"></input>
            </div>
            <div className="edit-words__group-line">
                <div className="group-line__left-text">List of words</div>
                <div className="group-line__right-text">+ Import from Dock, Excel file</div>
            </div>
            <div className="edit-words__list-of-words">
                {arr.map((el) => {
                    return <EditWordBlock counter={el} key={el}/>;
                })}
                <EditWordBlock type={"add"} counter={arr.length + 1}/>
            </div>
            <div className="edit-words__button">
                <button onClick={HandleSubmit}>Save</button>
            </div>

        </div>
    );
};

interface IEditWordBlock {
    counter: number,
    type?: string,
}

const EditWordBlock: FC<IEditWordBlock> = ({counter, type}) => {
    const defRef = useRef<HTMLInputElement>(null);
    const inputRef = useRef<HTMLInputElement>(null);
    return (
        <div className="edit-word-block__wrapper">
            <div className="edit-word-block__content">
                <div className="edit-word-block__content-header">
                    <div className="edit-word-block__content-header-item--counter">{counter}</div>
                    <div className="edit-word-block__content-header-item--delete-image">
                        <TrashIcon/>
                    </div>
                </div>
                <div className="edit-word-block__content-main">
                {type == "add"
                    ? <div className="edit-word-block__content-main-label">
                        <div className="edit-word-block__add-label">+Add a card</div>
                    </div>
                    : <div className="edit-word-block__group-of-inputs">
                        <div className="edit-word-block__group-of-inputs--items">
                            <WordInput inputRef={inputRef} secondRef={defRef}/>
                            <WordInput type={1} inputRef={defRef} secondRef={inputRef}/>
                        </div>

                        <div className="edit-word-block__image-block">
                            <GalleryIcon/>
                        </div>
                    </div>
                }
                </div>
            </div>
        </div>
    );
};

interface IWordInput {
    type?: number,
    inputRef: React.RefObject<HTMLInputElement>,
    secondRef?: React.RefObject<HTMLInputElement>
}

const WordInput: FC<IWordInput> = ({type = 0,inputRef, secondRef}) => {
    const [timer, setTimer] = useState<any>();
    const maxLength = type ? 100 : 50;
    const [letters, setLetters] = useState<number>(0);
    const placeHolder = type ? "Definition" : "Word";
    const [suggestion, setSuggestion] = useState<any[]>([]);
    
    const SearchDefenitionQuery = async () => {
        let term='';
        if(secondRef && secondRef.current){
            term = secondRef.current.value;
        }
        const response = await fetch(`https://localhost/api/WordMeanings/definition?term=${term}&lang=en&defLang=uk`);
        const data = await response.json();
        return data;
    };

    useEffect(() => {
        const handleFocus = () => {
          if (type && secondRef && secondRef.current && secondRef.current.value !== "") {
            SearchDefenitionQuery().then((sugg) => {
                 setSuggestion(sugg);
            });
          }
        };
        if(type)
        inputRef?.current?.addEventListener("focus", handleFocus);
        
        return () => {
          if(type)
          inputRef?.current?.removeEventListener("focus", handleFocus);
        };
      }, [type]);
      
    

    const SearchQuery = async (term: string) => {
        const response = await fetch(`https://localhost/api/Words/term?term=${term}&language=en`);
        const data = await response.json();
        return data;
    };

    

    const searchFunc = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const search = e.target.value;
        setLetters(search.length);
        if (search.length === 0) {
            setSuggestion([]);
        }
        if (search.length < 3) return 0;
        if (timer) clearTimeout(timer);
        setTimer(
            setTimeout(async () => {
                setSuggestion(type ? await SearchDefenitionQuery() : await SearchQuery(search));
            }, 1000)
        );
    };

    const HandleSuggestion = (event: React.MouseEvent<HTMLDivElement>, term: string) => {
        event.preventDefault();
        if(inputRef && inputRef.current){
            inputRef.current.value = term;
            setSuggestion([]);
            HandleFocus();
        }
    }
    const HandleOnBlue = () => {
        if(!type){
            HandleFocus();
            setSuggestion([]);
        }
        if(type){
            
        }
    }
    const HandleFocus = () => {
        if(secondRef && secondRef.current){
            secondRef.current.focus();
        }
    }

    useEffect(() => {
        setSuggestion([]);
    }, [inputRef?.current?.blur])

    return (
        <div className="word-input__wrapper">
            <div className="word-input__input-container">
                <div className="word-input__input-group-icon">
                    {type
                        ? <BookIcon/>
                        : <WordIcon/>
                    }
                    <input type="text" className="word-input" placeholder={placeHolder} ref={inputRef} onChange={(e) => searchFunc(e)} onKeyPress={event => {
                        if (event.key === 'Enter') {
                            HandleFocus();
                        }
                    }}/>
                </div>
                <div className="word-input__input-container--letters-counter">{letters} / {maxLength}</div>
            </div>
            <div className="word-input__suggestion-list">
                {suggestion.map(s => {
                    return <div key={s.id} className='word-input__suggestion-item' onClick={(event) =>{ 
                            HandleSuggestion(event, s.term)
                        }}>
                        {type 
                            ? <span>{s.term}</span>
                            : <span>
                                <strong>{inputRef.current?.value}</strong>
                                {s.term.slice(inputRef.current?.value.length)}
                              </span>
                        }
                        
                    </div>
                })}
            </div>
        </div>
    );
};